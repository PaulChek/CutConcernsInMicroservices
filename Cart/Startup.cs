using Cart.Model;
using Cart.Repos;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SharedCode;

namespace Cart {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services) {

            services.AddSingleton<MongoDbContext<ShoppingCart>>();
            services.AddSingleton<IRepo<ShoppingCart>, CartRepository<ShoppingCart>>();


            services.AddScoped<RabbitMqSetup.Message>();

            services.AddMassTransit(c => {
                c.AddConsumer<MessageConsumer>();
                c.UsingRabbitMq((context, config) => {
                    config.Host(Configuration["MassTransitSettings:Host"], "/", h => {
                        h.Username(Configuration["MassTransitSettings:User"]);
                        h.Password(Configuration["MassTransitSettings:Password"]);
                    });
                    config.ReceiveEndpoint(RabbitMqSetup.cartCreation, e => e.ConfigureConsumer<MessageConsumer>(context));
                });
            });
            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart", Version = "v1" });
            });

            services.AddHealthChecks();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart v1"));
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
