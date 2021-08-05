using CareHomeTaskManager.Core;
using CareHomeTaskManager.Core.Authentication;
using CareHomeTaskManager.Core.DataInterface;
using CareHomeTaskManager.DataAccess;
using CareHomeTaskManager.DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CareHomeTaskManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _settings = new AuthenticationSettings();
            configuration.GetSection("AuthenticationSettings").Bind(_settings);
            TokenSettings.ClockSkew = _settings.ClockSkew;
            TokenSettings.ValidAudience = _settings.ValidAudience;
            TokenSettings.ValidIssuer = _settings.ValidIssuer;
        }
        private readonly AuthenticationSettings _settings;

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "AllowAll";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
            services.AddTransient<ICareTaskRepository, CareHomeTaskManagerRepository>();
            services.AddTransient<ICareTaskManager, CareTaskManager>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserManager,UserManager>();
            services.ConfigureJwtAuthentication(
                _settings.Secret, _settings.ValidIssuer, _settings.ValidAudience
                );
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });
            services.AddDbContext<CareHomeTaskManagerContext>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Care Home Task Manager API",
                        Version = "v1",
                        Description = "<p>This API project is a part of Demo Care Home Task Management Solution</p>" +
                    " <p> All API endpoints returns Http Result Codes with appropriate explanation. </p>" +
                    "<p>For more info, please contact by emailing to <a href='mailto:techs.erdem@gmail.com'>techs.erdem@gmail.com </a></p>",
                        Contact = new OpenApiContact { Name = "Erdem ISIKDOGAN", Email = "techs.erdem@gmail.com" },
                        License = new OpenApiLicense { Name = "Erdem Isikdogan - Open Licence" }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CareHomeTaskManager.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    public sealed class AuthenticationSettings
    {
        public string Secret { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public bool RequireSignedTokens { get; set; }
        public bool ValidateLifeTime { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuerSigningkey { get; set; }
        public int ClockSkew { get; set; }
    }
    public static class TokenSettings
    {
        public static int ClockSkew { get; set; }
        public static string ValidIssuer { get; set; }
        public static string ValidAudience { get; set; }
    }
}
