using ASAPSystems.Task.Infrastructure.IEntityService.EntityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.IEntityService.UnitOfWorks
{
    public interface IUnitOfWork :IDisposable
    {
        #region Main Methods
        public int Commit();
        public int Commit<T>(T entity);
        #endregion
        public IUserEntityService User { get; }
        public IPersonEntityService Person { get; }
        public IAddressEntityService Address { get; }
    }
}
