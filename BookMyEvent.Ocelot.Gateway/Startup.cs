using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BookMyEvent.Ocelot.Gateway
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
            // sub => http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier

            // This will clear the default claims
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var authenticationScheme = "BookMyEventGatewayScheme";
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(authenticationScheme,options =>
                {
                    options.Authority = "https://localhost:5012";
                    options.Audience = "bookmyeventgateway";
                });
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           await app.UseOcelot();
           
        }
    }
}
