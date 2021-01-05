using BookMyEvent.Services.IntegrationEventPublisher.DbContexts;
using BookMyEvent.Services.IntegrationEventPublisher.Repositories;
using BookMyEvent.Services.IntegrationEventPublisher.Worker;
using BookMyEvent.Integration.MessagingBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BookMyEvent.Services.IntegrationEventPublisher
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
            //services.AddDbContext<IntegrationEventsDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventsDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());

            services.AddSingleton(new IntegrationEventRepository(optionsBuilder.Options));

            services.AddHostedService<EventPublisher>();

            services.AddSingleton<IMessageBus, AzServiceBusMessageBus>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
