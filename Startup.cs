using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirinWebApi.Database;
using FirinWebApi.Helper;
using FirinWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FirinWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string izinVerdigimKaynaklar = "_izinVerdigimKaynaklar";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<FirinWebApiDbContext>(opt => opt
                    .UseSqlServer(Configuration
                    .GetConnectionString("FirinDb")));

            //appsettins.json daki appsettings degerinden olusturuyor.
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            

            services.AddTransient<IMusteriService, MusteriService>();
            services.AddTransient<IAlinanUrunService, AlinanUrunService>();
            services.AddTransient<ISatilanUrunService, SatilanUrunService>();
            services.AddTransient<IIrsaliyeService, IrsaliyeService>();
            services.AddTransient<IKullaniciService, KullaniciService>();
            services.AddTransient<IFaturaService, FaturaService>();
            services.AddTransient<IOdemeService , OdemeService>();
            services.AddTransient<ITahsilatService, TahsilatService>();

            services.AddTransient<AdminFilter>();

            services.AddCors(opt =>
            {
                opt.AddPolicy(izinVerdigimKaynaklar,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod();
                                                     //   eski tur tarayýcýlarda 
                                                    //   falan bu 2sý lazým olabýlýyor            
                    });
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(izinVerdigimKaynaklar);

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
