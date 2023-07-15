using ASAPSystems.Task.Infrastructure.EntityService.Context;
using ASAPSystems.Task.Infrastructure.EntityService.EntityServices;
using ASAPSystems.Task.Infrastructure.IEntityService.EntityServices;
using ASAPSystems.Task.Infrastructure.IEntityService.UnitOfWorks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.EntityService.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        #region  Fields:
        private APPDbContext _AppDbContext;
        public ILogger<UnitOfWork> _Logger { get; }
        #endregion
        #region Ctor
        public UnitOfWork(APPDbContext AppDbContext, ILogger<UnitOfWork> logger)
        {
            _AppDbContext = AppDbContext;
            _Logger = logger;
        }
        #endregion
        #region  PROPS
        public IUserEntityService User => new UserEntityService(_AppDbContext, _Logger);
        public IPersonEntityService Person => new PersonEntityService(_AppDbContext, _Logger);
        public IAddressEntityService Address => new AddressEntityService(_AppDbContext, _Logger);
        #endregion
        #region Methods
        public int Commit()
        {
            return _AppDbContext.SaveChanges();

        }
        public int Commit<T>(T entity)
        {

            int result = default(int);
            try
            {
                result = _AppDbContext.SaveChanges();
            }
            catch (Exception exception)
            {

            }
            return result;
        }
        public void Dispose()
        {
            _AppDbContext.Dispose();
        }
        #endregion
    }
}
