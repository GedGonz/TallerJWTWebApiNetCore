using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace TallerJWTWebApiNetCore
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

            //generar una semilla
            string ClaveSecreta = "123456ABCDEabdebg";
            //Se convierte en bytes
            byte[] claveEnByte = Encoding.UTF8.GetBytes(ClaveSecreta);
            //nuget: System.IdentityModel.Tokens.Jwt
            var key = new SymmetricSecurityKey(claveEnByte);
            //Crea la credencial con el algoritmo HmacSha256
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Se activa la Autotizacion de tipo Bearer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "GedGonz",
                        ValidAudience = "GedGonz",
                        ValidateLifetime = true,
                        IssuerSigningKey = key
                    };
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Microsoft.AspNetCore.Authentication.JwtBearer

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
