using System;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using BookMyEvent.Integration.MessagingBus;
using BookMyEvent.Services.Ordering.Repositories;
using BookMyEvent.Services.Ordering.Messages;
using BookMyEvent.Services.Ordering.Entities;
using System.Collections.Generic;

namespace BookMyEvent.Services.Ordering.Messaging
{
    public class AzServiceBusConsumer : IAzServiceBusConsumer
    {
        private readonly string subscriptionName = "bookmyeventorder";
        private readonly IReceiverClient checkoutMessageReceiverClient;
        private readonly IReceiverClient orderPaymentUpdateMessageReceiverClient;

        private readonly IConfiguration _configuration;

        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly IMessageBus _messageBus;

        private readonly string checkoutMessageTopic;
        private readonly string orderPaymentRequestMessageTopic;
        private readonly string orderPaymentUpdatedMessageTopic;

        public AzServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, OrderRepository orderRepository, CustomerRepository customerRepository)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            // _logger = logger;
            _messageBus = messageBus;

            var serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");
            orderPaymentRequestMessageTopic = _configuration.GetValue<string>("OrderPaymentRequestMessageTopic");
            orderPaymentUpdatedMessageTopic = _configuration.GetValue<string>("OrderPaymentUpdatedMessageTopic");

            checkoutMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, checkoutMessageTopic, subscriptionName);
            orderPaymentUpdateMessageReceiverClient = new SubscriptionClient(serviceBusConnectionString, orderPaymentUpdatedMessageTopic, subscriptionName);
        }

        public void Start()
        {
            var messageHandlerOptions = new MessageHandlerOptions(OnServiceBusException) { MaxConcurrentCalls = 4 };

            checkoutMessageReceiverClient.RegisterMessageHandler(OnCheckoutMessageReceived, messageHandlerOptions);
            orderPaymentUpdateMessageReceiverClient.RegisterMessageHandler(OnOrderPaymentUpdateReceived, messageHandlerOptions);
        }

        private async Task OnCheckoutMessageReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);//json from service bus

            //save order with status not paid
            BasketCheckoutMessage basketCheckoutMessage = JsonConvert.DeserializeObject<BasketCheckoutMessage>(body);

            // Get or Add customer
            Customer customer = await _customerRepository.GetCustomerById(basketCheckoutMessage.UserId);
            if (customer == null)
            {
                // create new customer
                Customer newCustomer = new Customer
                {
                    CustomerId = basketCheckoutMessage.UserId,
                    FirstName = basketCheckoutMessage.FirstName,
                    LastName = basketCheckoutMessage.LastName,
                    Email = basketCheckoutMessage.Email,
                    Address = basketCheckoutMessage.Address,
                    ZipCode = basketCheckoutMessage.ZipCode,
                    City = basketCheckoutMessage.City,
                    Country = basketCheckoutMessage.Country
                };

                await _customerRepository.AddCustomer(newCustomer);

            }

            // Create new order object
            Guid orderId = Guid.NewGuid();

            Order order = new Order
            {
                UserId = basketCheckoutMessage.UserId,
                Id = orderId,
                OrderPaid = false,
                OrderPlaced = DateTime.Now,
                OrderTotal = basketCheckoutMessage.BasketTotal
            };

            order.OrderLines = new List<OrderLine>();

            // create OrderLines for each basketLine (event tickets)
            foreach (var bLine in basketCheckoutMessage.BasketLines)
            {
                OrderLine orderLine = new OrderLine
                {
                    OrderLineId = Guid.NewGuid(),
                    OrderId = orderId,
                    Price = bLine.Price,
                    TicketAmount = bLine.TicketAmount,
                    EventId = bLine.EventId,
                    EventName = bLine.EventName,
                    EventDate = bLine.EventDate,
                    VenueName = bLine.VenueName,
                    VenueCity = bLine.VenueCity,
                    VenueCountry = bLine.VenueCountry
                };
                order.OrderLines.Add(orderLine);
            }

            await _orderRepository.AddOrder(order);

            //send order payment request message
            OrderPaymentRequestMessage orderPaymentRequestMessage = new OrderPaymentRequestMessage
            {
                CardExpiration = basketCheckoutMessage.CardExpiration,
                CardName = basketCheckoutMessage.CardName,
                CardNumber = basketCheckoutMessage.CardNumber,
                OrderId = orderId,
                Total = basketCheckoutMessage.BasketTotal
            };

            try
            {
                await _messageBus.PublishMessage(orderPaymentRequestMessage, orderPaymentRequestMessageTopic);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task OnOrderPaymentUpdateReceived(Message message, CancellationToken arg2)
        {
            var body = Encoding.UTF8.GetString(message.Body);//json from service bus
            OrderPaymentUpdateMessage orderPaymentUpdateMessage =
                JsonConvert.DeserializeObject<OrderPaymentUpdateMessage>(body);

            await _orderRepository.UpdateOrderPaymentStatus(orderPaymentUpdateMessage.OrderId, orderPaymentUpdateMessage.PaymentSuccess);
        }

        private Task OnServiceBusException(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine(exceptionReceivedEventArgs);

            return Task.CompletedTask;
        }

        public void Stop()
        {
        }
    }
}
