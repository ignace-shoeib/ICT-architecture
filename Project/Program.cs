using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
namespace Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseKestrel();
            webBuilder.UseUrls("http://*:80");
            webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
            webBuilder.UseIISIntegration();
            webBuilder.UseStartup<Startup>();
        });
    }
}