using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UPD.EntityFramework.Seed;
using System;
using UPD.EntityFramework;

namespace EFCory.Migrator
{
    public class DeployService : IDeployService
    {
        #region • Init •

        //private readonly ILogger<DeployService> _logger;
        private readonly DeployOptions _deployOptions;
        private readonly DatabaseContext _dbContext;

        public DeployService(//ILoggerFactory loggerFactory,
            IOptions<DeployOptions> deployOptions,
            DatabaseContext databaseContext)
        {
            //_logger = loggerFactory.CreateLogger<DeployService>();
            _deployOptions = deployOptions?.Value;
            _dbContext = databaseContext;
        }

        #endregion

        public void Execute()
        {
            Log("Database deploy is STARTED.");

            Log($"Database ConntectionString >> {_dbContext?.Database?.GetDbConnection()?.ConnectionString}");

            if (_dbContext is null)
            {
                Log($"Configuration file should contain a connection string named");
                return;
            }

            try
            {
                if (_deployOptions.Migration)
                {
                    Log($"Databse Migration ..");
                    _dbContext.Database.Migrate();
                }

                if (_deployOptions.DataSeeding)
                {
                    Log($"Data Seeding ..");
                    SeedHelper.Create(_dbContext);
                }
            }
            catch (Exception ex)
            {
                Log("An error occured during migration of database:");
                Log(ex.ToString());
                Log("Database deploy is CANCELLED.");
                return;
            }

            Log("Database deploy is COMPLETED.");
        }

        private static void Log(string data)
        {
            Console.WriteLine();
            Console.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss}: {data}");
        }
    }
}

