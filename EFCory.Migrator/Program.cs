using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using UPD.EntityFramework;

namespace EFCory.Migrator
{
    class Program
    {
        private static void Main()
        {
            var serviceProvider = ConfigureServices().BuildServiceProvider();

            var deployService = serviceProvider.GetService<IDeployService>();

            deployService.Execute();

            Console.WriteLine("");
            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }

        private static IServiceCollection ConfigureServices()
        {
            IConfigurationRoot configurations = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            IServiceCollection services = new ServiceCollection();

            var dbConnectionString = configurations.GetConnectionString(SettingKeys.ConnectionString);
            services.AddDbContext<DatabaseContext>(options => options.ConfigDatabaseContext(dbConnectionString));

            services.Configure<DeployOptions>(configurations.GetSection(SettingKeys.DeploySection));

            services.AddSingleton<IDeployService, DeployService>();

            services.AddTransient<Program>();

            return services;
        }
    }
}
