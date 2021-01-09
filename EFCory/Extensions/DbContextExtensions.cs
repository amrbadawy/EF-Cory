using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EFCory
{
    public static class DbContextExtensions
    {
        public static DbContextOptionsBuilder ConfigDatabaseContext(this DbContextOptionsBuilder optionsBuilder, string connectionString)
        {
            return optionsBuilder.UseSqlServer(connectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

        public static void DisplayChanges(this DbContext dbContext)
        {
            Console.WriteLine();

            var entries = dbContext.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                Console.WriteLine($"{entry.ToString()}");
                //Console.WriteLine($"{entry.Entity} - {entry.State} => {entry.ToString()}");
            }

            Console.WriteLine();
        }

        public static async Task ResetDatabase(this DbContext dbContext)
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }
    }
}