using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Service1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestServiceController : ControllerBase
    {
        private readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestServiceController> _logger;

        public TestServiceController(ILogger<TestServiceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Summaries;
        }
        
        // [HttpGet]
        // public string Get(int id)
        // {
        //     return Summaries[id];
        // }
    }
}
