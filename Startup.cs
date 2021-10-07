using HSP_BusinessLogic;
using HSP_Models.Models;
using HSP_Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeServiceProvieders_MVC_UI
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
            services.AddControllersWithViews();
            services.AddMvc();
            services.AddDbContext<HomeServiceProviderContext>();
            services.AddScoped(typeof(Igeneric<>), typeof(GenericRepository<>));
            services.AddScoped<HSP_UserBAL>();
            services.AddScoped<HSP_ServiceProviderBAL>();
            services.AddScoped<HSP_ServicesProvidedBAL>();
            services.AddScoped<HSP_CustomerOrderBAL>();
            services.AddScoped<HSP_ServiceFeedbackBAL>();
            services.AddScoped<HSP_CustomerBillBAL>();
            services.AddScoped<INonGenric, NonGenericRepo>();
            services.AddSession();//-Registeration-//
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })


                .AddJwtBearer(opt =>
                {

                    opt.SaveToken = true;
                    opt.RequireHttpsMetadata = false;

                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "abc",
                        ValidAudience = "abc",

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("abcdefghijklmnopqrst"))
                    };


                });
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(); //middleware
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                //    pattern: "{controller=CustomerdetailsUI}/{action=showuserdetails}/{id?}");
                pattern: "{controller=Home}/{action=Index}/{id?}");
                //pattern: "{controller=ServiceProvidedUI}/{action=showSPdetails}/{id?}");
                // pattern: "{controller=CustomerBillUI}/{action=showAllBilldetails}/{id?}");
                //pattern: "{controller=ServiceFeedbackUI}/{action=showuserdetails}/{id?}");
                //pattern: "{controller=ServiceProviderdetailsUI}/{action=insertServiceProvider}/{id?}");

            });
        }
    }
}
