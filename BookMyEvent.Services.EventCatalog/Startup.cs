using AutoMapper;
using BookMyEvent.Integration.MessagingBus;
using BookMyEvent.Services.EventCatalog.DbContexts;
using BookMyEvent.Services.EventCatalog.Repositories;
using BookMyEvent.Services.EventCatalog.Services;
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
using System;

namespace BookMyEvent.Services.EventCatalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<EventCatalogDbContext>(
            //    options => options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection"),
            //        providerOptions => providerOptions.EnableRetryOnFailure(
            //            maxRetryCount: 10,
            //            maxRetryDelay: TimeSpan.FromSeconds(30),
            //            errorNumbersToAdd: null)
            //        )
            //    );

            services.AddDbContext<EventCatalogDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    providerOptions => providerOptions.EnableRetryOnFailure()));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IEventRepository, EventRepository>();

            // provides access to IEventRepository and IEventLogRepository
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();

            var requireAuthenticationUserPolicy = new AuthorizationPolicyBuilder()
                           .RequireAuthenticatedUser()
                           .Build();

            //This way we can enforce authentication on all controllers
            services.AddControllers(configure =>
            {
                configure.Filters.Add(new AuthorizeFilter(requireAuthenticationUserPolicy));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Event Catalog API", Version = "v1" });
            });


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // This way middleware will know, where to find well known document
                    options.Authority = "https://localhost:5012";
                    // only token with audience with bookmyevent value will only be allowed
                    options.Audience = "eventcatalog";
                });
            //Authorization Policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanRead", policy => policy.RequireClaim("scope", "eventcatalog.read", "eventcatalog.write"));
            });
        }

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Catalog API V1");

            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
