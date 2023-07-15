using ASAPSystems.Task.Core.Entity.Entities;
using ASAPSystems.Task.Infrastructure.IEntityService.BaseEntityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.IEntityService.EntityServices
{
    public interface IUserEntityService : IBaseRepository<User>
    {
        User GetUserByCredential(string UserName, string Password);
        bool AddUser(User user);
    }
}
