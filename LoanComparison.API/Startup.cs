using IdentityModel.Client;
using LoanComparison.API.Extensions;
using LoanComparison.Common.DTO;
using LoanComparison.Service.Services;
using LoanComparison.Service.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;

namespace LoanComparison.API
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
            //add cors-allow any header, method and origin
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

                    });
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LoanComparison.API", Version = "v1" });
            });
            //register token service in IdentityModel 
            services.AddAccessTokenManagement(options =>
            {
                options.Client.Clients.Add("oauth", new ClientCredentialsTokenRequest
                {
                    Address = Configuration.GetValue<string>("OAuth:Endpoint"),
                    ClientId = Configuration.GetValue<string>("OAuth:ClientId"),
                    ClientSecret = Configuration.GetValue<string>("OAuth:ClientSecret"),
                    Scope = Configuration.GetValue<string>("OAuth:Scope")
                });
            });
            //register httpclient configurations to use token service in IdentityModel 
            services.AddClientAccessTokenClient("client", configureClient: client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("LoanCalculatorApiBaseUrl"));
            });

            //configure dependancy injection
            services.AddScoped<ILoanComparisonService, LoanComparisonService>();
            services.AddScoped<IHttpService, HttpService>();

            //register loan calculator request parameter configuration
            services.Configure<LoanCalculatorRequestParam>(Configuration.GetSection("LoanCalculatorRequestParam"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //add log4Net
            loggerFactory.AddLog4Net("log4net.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoanComparison.API v1"));
            }

            app.ConfigureExceptionHandler(loggerFactory);

            app.UseHttpsRedirection();

            app.UseRouting();

            //add CORS
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
