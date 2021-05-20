using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
                for(int i = 1; i< 15; i++)
                {
                    Console.WriteLine(await new CalculateIqamaRenewalFees().Execute(i));
                }

                var equation = "{yearlyFee} + ((1-(1+ {cumulativeDiscount} )^ -( {yearsCount} - 1 )) / {cumulativeDiscount} ) * {yearlyFee} ";
                var values = new Dictionary<string, string>()
                {
                    { "yearlyFee", "100000" },
                    { "cumulativeDiscount", "(2/100)" },
                    { "yearsCount", "2" }
                };
                var finalEquation = FormatPlaceholder(equation, values);

                var result2 = await CSharpScript.EvaluateAsync("100000 + ((1-(1+ 52 )^ -( 2 - 1 )) /2 ) * 100000");


                var x = string.Equals("a", "b", StringComparison.OrdinalIgnoreCase);
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

        static string FormatPlaceholder(string input, Dictionary<string, string> fields)
        {
            Regex regex = new Regex(@"\{([^\}]+)\}", RegexOptions.Compiled);
            if (fields == null)
                return input;

            return regex.Replace(input, match =>
            {
                var placeholder = match.Groups[1].Value;
                return fields.TryGetValue(placeholder, out string value) ? fields[placeholder] : "{ " + placeholder + " }";
            });
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

        public class CalculateIqamaRenewalFees
        {
            public CalculateIqamaRenewalFees()
            {

            }

            public Task<double> Execute(int yearsCount)
            {
                var yearlyFee = 100_000.00;
                var cumulativeDiscount = 0.02;

                //var x = yearlyFee + ((1-(1+ cumulativeDiscount)^ -(yearsCount-1)) / cumulativeDiscount ) * yearlyFee
                var result = yearlyFee + ((1 - Math.Pow((1 + cumulativeDiscount), -(yearsCount - 1))) / cumulativeDiscount) * yearlyFee;
                result = Math.Round(result);
                return Task.FromResult(result);


            }
        }
    }
}