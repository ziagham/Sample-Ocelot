using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.IO;

namespace Gateway
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;

        public Startup(IWebHostEnvironment env)
        {
            var ocelotJson = new JObject();
            foreach (var jsonFilename in Directory.EnumerateFiles("Configuration", "ocelot.*.json", SearchOption.AllDirectories))
            {
                using (StreamReader fi = File.OpenText(jsonFilename))
                {
                    var json = JObject.Parse(fi.ReadToEnd());
                    ocelotJson.Merge(json, new JsonMergeSettings
                    {
                        MergeArrayHandling = MergeArrayHandling.Union
                    });
                }
            }

            File.WriteAllText("ocelot.json", ocelotJson.ToString());

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOcelot(Configuration)
                .AddConsul();
                // .AddConfigStoredInConsul();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            
            app
                .UseOcelot()
                .Wait();
        }
    }
}