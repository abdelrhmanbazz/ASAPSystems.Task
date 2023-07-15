using ASAPSystems.Task.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.EntityService.Context
{
    public class APPDbContext :DbContext
    {
        private IConfiguration configuration;
        public APPDbContext()
        {
            DoMigration = false;
        }
        public APPDbContext(DbContextOptions<APPDbContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Address> Address { get; set; }

        public bool DoMigration { get; set; } = false;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!DoMigration)
            {
                if (optionsBuilder.IsConfigured)
                {

                    optionsBuilder.UseSqlServer("Server = localhost; Database = ASPASysytemDB; Trusted_Connection = True; TrustServerCertificate = true");
                }
            }

        }
    }
}
