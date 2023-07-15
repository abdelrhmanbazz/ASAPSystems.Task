using ASAPSystems.Task.Core.Entity.Entities;
using ASAPSystems.Task.Infrastructure.EntityService.BaseEntityServices;
using ASAPSystems.Task.Infrastructure.EntityService.Context;
using ASAPSystems.Task.Infrastructure.IEntityService.EntityServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.EntityService.EntityServices
{
    public class AddressEntityService : BaseRepository<Address>, IAddressEntityService
    {
        #region Prop
        public APPDbContext _AppDbContext { get; }
        private ILogger _Logger { get; }
        #endregion
        #region CTORS :
        public AddressEntityService(APPDbContext appDbContext, ILogger Logger) : base(appDbContext, Logger)
        {
            _AppDbContext = appDbContext;
            _Logger = Logger;
        }
        #endregion
        #region Methods :
        public bool InsertAddress(Address address)
        {
            bool isInserted = default(bool);
            try
            {
                isInserted = Insert(address);
            }
            catch (Exception)
            {

                throw;
            }
            return isInserted;
        }

        #endregion
    }
}
