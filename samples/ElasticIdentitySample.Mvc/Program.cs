using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;
using Serilog.Events;

namespace ElasticIdentitySample.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                                    .Enrich.FromLogContext()
                                    .WriteTo.Console()
                                    .WriteTo.Seq("http://localhost:5341")
                                    .CreateLogger();

            try
            {
                Log.Information("Starting up");
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                //.UseApplicationInsights()
                .Build();
    }
}
