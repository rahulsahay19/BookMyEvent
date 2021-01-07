using BookMyEvent.Web.Models;
using BookMyEvent.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BookMyEvent.Web
{
    public class Startup
    {
        private readonly IHostEnvironment environment;
        private readonly IConfiguration config;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            config = configuration;
            this.environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

            var builder = services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy));
            });

            if (environment.IsDevelopment())
                builder.AddRazorRuntimeCompilation();

            //services.AddGrpcClient<Events.EventsClient>(
            //    o => o.Address = new Uri(config["ApiConfigs:EventCatalog:Uri"]));

            services.AddHttpClient<IEventCatalogService, EventCatalogService>(c =>
               c.BaseAddress = new Uri(config["ApiConfigs:EventCatalog:Uri"]));
            services.AddHttpClient<IShoppingBasketService, ShoppingBasketService>(c =>
                c.BaseAddress = new Uri(config["ApiConfigs:ShoppingBasket:Uri"]));
            services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(config["ApiConfigs:Order:Uri"]));
            services.AddHttpClient<IDiscountService, DiscountService>(c =>
                c.BaseAddress = new Uri(config["ApiConfigs:Discount:Uri"]));


            services.AddSingleton<Settings>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
           {
               options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               options.Authority = "https://localhost:5012/";
               options.ClientId = "bookmyevent";
               options.ResponseType = "code";
               options.SaveTokens = true;
               options.ClientSecret = "d1ee8859-6e00-45f9-b749-3ae4124299a0";
               options.GetClaimsFromUserInfoEndpoint = true;
               options.Scope.Add("shoppingbasket.fullaccess");
           });

            //var storageAccount = CloudStorageAccount.Parse(config["AzureQueues:ConnectionString"]);

            ////One way client means we can only send messages not receive
            //services.AddRebus(c => c
            //    .Transport(t => t.UseAzureStorageQueuesAsOneWayClient(storageAccount))
            //    .Routing(r => r.TypeBased().Map<PaymentRequestMessage>(
            //        config["AzureQueues:QueueName"]))
            //);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           // app.ApplicationServices.UseRebus();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=EventCatalog}/{action=Index}/{id?}");
            });
        }
    }
}
