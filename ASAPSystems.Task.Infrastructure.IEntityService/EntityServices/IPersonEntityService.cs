using ASAPSystems.Task.Core.Entity.Entities;
using ASAPSystems.Task.Infrastructure.IEntityService.BaseEntityServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Infrastructure.IEntityService.EntityServices
{
    public interface IPersonEntityService : IBaseRepository<Person>
    {
        bool InsertPerson(Person Person);
        Person GetPersonById(int PersonId);
        bool UpdatePerson(Person Person);
        bool DeletePerson(Person person);
    }
}
