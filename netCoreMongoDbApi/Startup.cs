using System.ComponentModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netCoreMongoDbApi.Domain.Repositories;
using netCoreMongoDbApi.Persistence.Contexts;
using netCoreMongoDbApi.Persistence.Repository;
using AutoMapper;
using netCoreMongoDbApi.Services;
using netCoreMongoDbApi.Domain.Services;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using netCoreMongoDbApi.Persistence.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using netCoreMongoDbApi.Controllers.Config;

namespace netCoreMongoDbApi
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
            services.AddControllers()
            .ConfigureApiBehaviorOptions(option =>
            {
                option.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });
            services.AddMvc();
            services.Configure<Settings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetConnectionString("DefaultConnection");
                options.Database
                    = Configuration.GetSection("MongoSettings:Database").Value;
            });
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "netCoreMongoDb-templete API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Tomasz Szydlowski",
                        Url = new Uri("https://github.com/TomaszSzydlowski/.netCoreMongoDbApi-Templete"),
                        Email = "Tomasz.Piotr.Szydlowski@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT"
                    }
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
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

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Students Demo Templete");
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
