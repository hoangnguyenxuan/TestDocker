using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace TestDocker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseKestrel(options =>
            {
                options.Listen(IPAddress.Any, 443, listenOptions =>
                {
                    var configuration = (IConfiguration)options.ApplicationServices.GetService(typeof(IConfiguration));

                    listenOptions.UseHttps("cert.pfx", configuration["certPassword"]);
                });
            }).UseStartup<Startup>().Build();
    }
}
