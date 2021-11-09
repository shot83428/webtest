using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Output("Application Start.");
            var webHost = CreateHostBuilder(args);
            Output("Build WebHost");
            var WebHostBuilder = webHost.Build();
            Output("Run WebHost");
            WebHostBuilder.Run();
            Output("Application End.");
        }
        // origin test
        // public static IHostBuilder CreateHostBuilder(string[] args) =>
        //     Host.CreateDefaultBuilder(args)
        //         .ConfigureWebHostDefaults(webBuilder =>
        //         {
        //             webBuilder.UseStartup<Startup>();
        //         });

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            Output("Create WebHost Builder");
            var webHostBuilder = WebHost.CreateDefaultBuilder(args).
            ConfigureServices(services => { Output("webHostBuilder.ConfigureServices - Called"); }).
            Configure(App => { Output("webHostBuilder.Configure - Called"); }).
            UseStartup<Startup>();

            // Host.CreateDefaultBuilder(args)
            //     .ConfigureServices(services =>
            //     {
            //         Output("webHostBuilder.ConfigureServices - Called");
            //     })
            //     .Configure(app =>
            //     {
            //         Output("webHostBuilder.Configure - Called");
            //     })
            //     .UseStartup<Startup>();

            Output("Build WebHost");
            //var webHost = webHostBuilder.Build();
            
            return webHostBuilder;
        }
        public static void Output(string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] {message}");
        }
    }
}
