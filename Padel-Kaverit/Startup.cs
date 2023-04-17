using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Padel_Kaverit.Middleware;
using Padel_Kaverit.Models;
using Padel_Kaverit.Repositories;
using Padel_Kaverit.Services;
using ReservationSystem.Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Middleware;

namespace Padel_Kaverit
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
            services.AddCors(options =>
            {

                options.AddDefaultPolicy(
                    policy =>
                    {
                     policy.SetIsOriginAllowed(origin => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                       policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                       
                    });
              
            });


         services.AddControllers();
            services.AddDbContext<PadelContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("PadelDB")));
            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
           services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IForumPostService, ForumPostService>();
           services.AddScoped<IForumPostRepository, ForumRepository>();
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IGamesReposotory, GamesRepository>();

            


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Padel API",
                    Description = "An ASP.NET Web API for managing Padel profile"
                });
               var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            });
            services.AddAzureAppConfiguration();


        }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PadelContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            db.Database.EnsureCreated();
            app.UseHttpsRedirection();
            app.UseAzureAppConfiguration();

            app.UseRouting();
          app.UseCors();
            
           app.UseMiddleware<ApiKeyMiddleware>();
           app.UseAuthentication();
           app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


 