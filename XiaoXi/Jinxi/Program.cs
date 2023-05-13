using Com.Ctrip.Framework.Apollo;
using Com.Ctrip.Framework.Apollo.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Jinxi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

           Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                webBuilder.ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddApollo(builder.Build().GetSection("apollo"))
                    .AddDefault()
                    .AddNamespace("appsettings",ConfigFileFormat.Json);
                })
                    .UseStartup<Startup>();
                });

    }
}
