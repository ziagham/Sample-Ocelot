using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Consul;

namespace Service1
{
    public static class ConsulExtension 
    {
        public static IServiceCollection AddConsul(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var options = new ConsulOptions();
            configuration.GetSection(nameof(ConsulOptions)).Bind(options);

            serviceCollection.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                if (!string.IsNullOrEmpty(options.Host))
                {
                    consulConfig.Address = new Uri(options.Host);
                }
            }));

            return serviceCollection;
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app, IConfiguration configuration)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var logger = app.ApplicationServices.GetRequiredService<ILoggerFactory>().CreateLogger("AppExtensions");
            var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();

            var options = new ConsulOptions();
            configuration.GetSection(nameof(ConsulOptions)).Bind(options);

            if (!(app.Properties["server.Features"] is FeatureCollection features))
            {
                return app;
            }

            var addresses = features.Get<IServerAddressesFeature>();
            var address = addresses.Addresses.First();

            if (!options.Enabled)
                return app;

            Guid serviceId = Guid.NewGuid();
            string consulServiceID = $"{options.ServiceName}-{serviceId}";
            Uri uri = new Uri(address);

            var registration = new AgentServiceRegistration()
            {
                ID = consulServiceID,
                Name = options.ServiceName,
                Address = $"{uri.Host}",
                Port = uri.Port
            };

            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            consulClient.Agent.ServiceRegister(registration).ConfigureAwait(true);

            lifetime.ApplicationStopping.Register(() =>
            {
                logger.LogInformation("Unregistering from Consul");
                consulClient.Agent.ServiceDeregister(registration.ID).ConfigureAwait(true);
            });

            return app;
        }
    }
}