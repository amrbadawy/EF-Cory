using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using UPD.EntityFramework;

namespace EFCory
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder().Build();
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            try
            {
                //var blogService = services.GetRequiredService<Blogs.IBlogService>();
                //await blogService.Test();

                var kpiService = services.GetRequiredService<KPIs.IKpiService>();
                await kpiService.Test();

                Console.WriteLine("Done");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            Console.ReadLine();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("MyDatabase");

            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging(configure => configure.AddConsole());
                    services.AddDbContext<DatabaseContext>(options => options.ConfigDatabaseContext(connectionString));
                    services.AddSingleton<Blogs.IBlogService, Blogs.BlogService>();
                    services.AddSingleton<KPIs.IKpiService, KPIs.KpiService>();
                });

            return builder;
        }
    }
}