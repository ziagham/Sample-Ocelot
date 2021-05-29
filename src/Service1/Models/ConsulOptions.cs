using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Service1
{
    public class ConsulOptions 
    {
        public string Host { get; set; }
        public string ServiceName { get; set; }
        public bool Enabled { get; set; }
    }
}