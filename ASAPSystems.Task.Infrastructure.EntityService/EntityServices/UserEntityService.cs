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
    public class UserEntityService : BaseRepository<User>, IUserEntityService
    {
        #region Prop
        public APPDbContext _AppDbContext { get; }
        private ILogger _Logger { get; }
        #endregion
        #region CTORS :
        public UserEntityService(APPDbContext appDbContext, ILogger Logger) : base(appDbContext, Logger)
        {
            _AppDbContext = appDbContext;
            _Logger = Logger;
        }
        #endregion
        #region Methods :

        public bool AddUser(User user)
        {
            bool isInserted = default (bool);
            try
            {
                isInserted = Insert(user);
            }
            catch (Exception)
            {

                throw;
            }
            return isInserted;
        }
        public User GetUserByCredential(string UserName, string Password)
        {
            User user = new User();

            try
            {

                user = Get(x => x.UserName == UserName && x.Password == Password);
            }
            catch (Exception)
            {

                throw;
            }
            return user;
        }
        #endregion
    }
}
