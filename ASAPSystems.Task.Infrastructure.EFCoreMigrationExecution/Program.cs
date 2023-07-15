using ASAPSystems.Task.Infrastructure.EntityService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.EFCoreMigrationExecution
{
    class Program
    {
        public static void Main(string[] args)
        {
            #region VARs
            var configurationBuilder = new ConfigurationBuilder()
                                      .SetBasePath(Directory.GetCurrentDirectory())
                                      .AddJsonFile("appsettings.json", true, true);

            var configuration = configurationBuilder.Build();

            string connectionString = configuration.GetConnectionString("DBConString");
            #endregion
            try
            {
                Console.WriteLine("Start Connect To DB To Apply Migration");
                using (APPDbContext sc = CreateDbContext(null, configuration, connectionString))
                {
                    sc.DoMigration = true;
                    sc.Database.Migrate();
                    int c = 0;
                }
                Console.WriteLine("End Migration");
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString() + "," + exception.StackTrace);
                Console.ReadLine();
            }
        }
        public static APPDbContext CreateDbContext(string[] args, IConfigurationRoot configuration, string connectionString)
        {

            DbContextOptionsBuilder<APPDbContext> optionsBuilder = new DbContextOptionsBuilder<APPDbContext>()
                .UseSqlServer(connectionString);

            return new APPDbContext(optionsBuilder.Options);
        }
    }
}
