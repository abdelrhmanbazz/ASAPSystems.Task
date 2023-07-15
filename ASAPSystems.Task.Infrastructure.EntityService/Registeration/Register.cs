using ASAPSystems.Task.Infrastructure.EntityService.Context;
using ASAPSystems.Task.Infrastructure.EntityService.UnitOfWorks;
using ASAPSystems.Task.Infrastructure.IEntityService.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.EntityService.Registeration
{
    public static class Register
    {
        public static void RegisterUnitOfWork(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<APPDbContext>(opt => opt.UseSqlServer(conf.GetConnectionString("DBConString")));
            services.AddTransient<IUnitOfWork, UnitOfWorks.UnitOfWork>();
        }
    }
}
