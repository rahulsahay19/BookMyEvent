using BookMyEvent.Services.Payment.Model;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Payment.Services
{
    public interface IExternalGatewayPaymentService
    {
        Task<bool> PerformPayment(PaymentInfo paymentInfo);
    }
}
