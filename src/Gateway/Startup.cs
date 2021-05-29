// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Microsoft.Extensions.Logging;
// using Microsoft.OpenApi.Models;
// using Ocelot.DependencyInjection;
// using Ocelot.Middleware;

// namespace Gateway
// {
//     public class Startup
//     {
//         public Startup(IConfiguration configuration)
//         {
//             Configuration = configuration;
//         }

//         public IConfiguration Configuration { get; }

//         public void ConfigureServices(IServiceCollection services)
//         {
//             services.AddOcelot();
//         }

//         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
//         {
//             app.UseOcelot().Wait();
//         }
//     }
// }
