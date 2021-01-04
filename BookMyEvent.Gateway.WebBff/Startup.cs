using AutoMapper;
using BookMyEvent.Gateway.WebBff.Services;
using BookMyEvent.Gateway.WebBff.Url;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Gateway.WebBff
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();

            services.AddOptions();
            services.Configure<ServiceUrls>(Configuration.GetSection("ServiceUrls"));

            services.AddHttpClient<ICatalogService, CatalogService>(c =>
                c.BaseAddress = new Uri(Configuration["ServiceUrls:EventCatalog"]));

            services.AddHttpClient<IBasketService, BasketService>(c =>
                c.BaseAddress = new Uri(Configuration["ServiceUrls:ShoppingBasket"]));

            services.AddHttpClient<IDiscountService, DiscountService>(c =>
                c.BaseAddress = new Uri(Configuration["ServiceUrls:Discount"]));

            services.AddHttpClient<IOrderService, OrderService>(c =>
                c.BaseAddress = new Uri(Configuration["ServiceUrls:Ordering"]));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GloboTicket Gateway BFF for Web API", Version = "v1" });
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book My Event Gateway BFF for Web API V1");

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
