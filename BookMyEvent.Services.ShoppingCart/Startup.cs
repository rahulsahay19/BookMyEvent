using AutoMapper;
using BookMyEvent.Integration.MessagingBus;
using BookMyEvent.Services.ShoppingCart.DbContexts;
using BookMyEvent.Services.ShoppingCart.Repositories;
using BookMyEvent.Services.ShoppingCart.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace BookMyEvent.Services.ShoppingCart
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var requireAuthenticationUserPolicy = new AuthorizationPolicyBuilder()
                           .RequireAuthenticatedUser()
                           .Build();

            ////This way we can enforce authentication on all controllers
            services.AddControllers(configure =>
            {
                configure.Filters.Add(new AuthorizeFilter(requireAuthenticationUserPolicy));
            });
            // services.AddControllers();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  // This way middleware will know, where to find well known document
                  options.Authority = "https://localhost:5012";
                  // only token with audience with bookmyevent value will only be allowed
                  options.Audience = "shoppingbasket";
              });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
           // services.AddHostedService<ServiceBusListener>();
            var optionsBuilder = new DbContextOptionsBuilder<ShoppingCartDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            services.AddSingleton(new BasketLinesIntegrationRepository(optionsBuilder.Options));

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketLinesRepository, BasketLinesRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IBasketChangeEventRepository, BasketChangeEventRepository>();

            services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();

            services.AddHttpClient<IEventCatalogService, EventCatalogService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiConfigs:EventCatalog:Uri"]));

            services.AddHttpClient<IDiscountService, DiscountService>(c =>
                c.BaseAddress = new Uri(Configuration["ApiConfigs:Discount:Uri"]))
                .AddPolicyHandler(GetRetryPolicy())
                .AddPolicyHandler(GetCircuitBreakerPolicy());

            services.AddDbContext<ShoppingCartDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping Basket API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shopping Cart API V1");

            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(5,
                    retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(1.5, retryAttempt) * 1000),
                    (_, waitingTime) =>
                    {
                        Console.WriteLine("Retrying due to Polly retry policy");
                    });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(15));
        }
    }
}
