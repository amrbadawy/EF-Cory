using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using UPD.EntityFramework;

namespace EFCory.Migrator
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString(SettingKeys.ConnectionString);

            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.ConfigDatabaseContext(connectionString);

            return new DatabaseContext(builder.Options);
        }
    }
    // nned to be refined by 
    // https://blog.tonysneed.com/2018/12/20/idesigntimedbcontextfactory-and-dependency-injection-a-love-story/
}