using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace web
{
    public class Startup
    {
        public Startup()
        {
            Program.Output("Startup Constructor - Called.");
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Program.Output("Startup ConfigureServices - Called.");
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // week 1 test 
        // public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        // {
        //     if (env.IsDevelopment())
        //     {
        //         app.UseDeveloperExceptionPage();
        //     }

        //     app.UseRouting();

        //     app.UseEndpoints(endpoints =>
        //     {
        //         endpoints.MapGet("/", async context =>
        //         {
        //             await context.Response.WriteAsync("Hello World!");
        //         });
        //     });
        // }
        //day 2
        public void Configure(IApplicationBuilder app, IHostApplicationLifetime appLifetime)
        {
            appLifetime.ApplicationStarted.Register(() => { Program.Output("ApplicationLifetime - Started."); });
            appLifetime.ApplicationStopping.Register(() => { Program.Output("ApplicationLifetime - Stopping"); });
            appLifetime.ApplicationStopped.Register(() => { Thread.Sleep(5 * 1000); Program.Output("ApplicationLifetime - Stopped"); });
            app.Run(async (context)=>{await context.Response.WriteAsync("HelloWorld!");});
            
            var thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(5 * 1000);
                Program.Output("Trigger stop WebHost");
                appLifetime.StopApplication();
            }));
            thread.Start();
            
            Program.Output("Startup.Configure - Called");

        }
    }
}
