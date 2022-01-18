using GyanDyan.DataAccess;
using GyanDyan.Services;
using GyanDyan.Services.Interfaces;
using GyanDyan.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GyanDyan
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors(options => {
                options.AddPolicy(StaticProvider.FrontendCorsPolicy, builder => {
                    builder.WithOrigins(
                            Configuration.GetSection(StaticProvider.AllowedOrigins).Get<string[]>()
                            )
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            services.AddDbContext<Context>(
                options => options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IRequirement, RequirementService>();
            services.AddScoped<IRequirementTranscation, RequirementTransaction>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(Configuration.GetSection("AuthConfig:ServerSecrects").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy(StaticProvider.StudentPolicy, policy => policy.RequireClaim("Roles", "Student"));
                options.AddPolicy(StaticProvider.VolunteerPolicy, policy => policy.RequireClaim("Roles", "Volunteer"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseRouting();
            app.UseCors(StaticProvider.FrontendCorsPolicy);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
