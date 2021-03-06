using CheckoutAPI.AcquiringBank;
using CheckoutAPI.DataStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Npgsql;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace CheckoutAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=mysecretpassword;Database=postgres";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAcquiringBank, MockBank>();
            //services.AddSingleton<IPaymentStorage, InMemoryDictionaryPaymentStore>();
            services.AddSingleton<IPaymentRequestValidator, PaymentRequestValidator>();

            services.AddSingleton<IDbConnection, NpgsqlConnection>(x => new NpgsqlConnection(connString));
            services.AddSingleton<IPaymentStorage, PostgresStorage>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen(generationOptions =>
            {
                generationOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Checkout code test API",
                    Description = "An API for processing and retrieving payments.",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                generationOptions.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(uiOptions =>
            {
                uiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkout code test API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
