using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pass_generation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseMetricsWebTracking(options => 
            {
                options.OAuth2TrackingEnabled = true;
                
            })
            .UseMetricsEndpoints(options =>
            {
                options.EnvironmentInfoEndpointEnabled = false;
                options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                options.MetricsEndpointOutputFormatter = new MetricsPrometheusProtobufOutputFormatter();

            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
