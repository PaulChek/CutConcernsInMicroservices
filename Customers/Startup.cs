using Customers.Models;
using Customers.Repos;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Customers {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {

            services.AddMassTransit(conf => conf.UsingRabbitMq((context, config) => {

                config.Host(Configuration["MassTransit:Host"], "/", h => {
                    h.Password(Configuration["MassTransit:Password"]);
                    h.Username(Configuration["MassTransit:Username"]);
                });

            })
            );
            services.AddMassTransitHostedService();

            services.AddSingleton<MongoContext<Customer>>();
            services.AddSingleton<IRepository<Customer>, Repository<Customer>>();

            services.AddMediatR(typeof(Startup).Assembly);


            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customers", Version = "v1" });
            });
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapHealthChecks("healthcheck");
                endpoints.MapControllers();
            });
        }
    }
}
