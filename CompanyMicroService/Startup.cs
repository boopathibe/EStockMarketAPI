using CompanyMicroService.Models;
using CompanyMicroService.Repository;
using CompanyMicroService.Services;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService
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
            services.Configure<CompanyDatabaseSettings>(
                Configuration.GetSection(nameof(CompanyDatabaseSettings)));

            services.AddSingleton<ICompanyDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<CompanyDatabaseSettings>>().Value);

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();

            var configSection = Configuration.GetSection("ServiceBus");
            var connectionUri = configSection.GetSection("ConnectionUri").Value;
            var usename = configSection.GetSection("Username").Value;
            var password = configSection.GetSection("Password").Value;

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(connectionUri), h =>
                    {
                        h.Username(usename);
                        h.Password(password);
                    });
                });
            });
            services.AddMassTransitHostedService(true);

            services.AddControllers();
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddSwaggerGen(options =>
           {
               options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
               {
                   Title = "EStock Company Service API",
                   Version = "v1",
                   Description = "Estock company micro services for CRUD",
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

            app.UseRouting();
            app.UseCors();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseAuthorization();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "EStock Company Services"));
        }
    }
}
