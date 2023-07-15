using ASAPSystems.Task.Infrastructure.EntityService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.EFCoreMigrationExecution
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<APPDbContext>
    {
        public APPDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<APPDbContext>();
            var connectionString = configuration.GetConnectionString("DBConString");
            builder.UseSqlServer(connectionString);

            return new APPDbContext(builder.Options);
        }
    }
}
