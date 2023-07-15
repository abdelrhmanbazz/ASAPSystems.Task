using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.IApplication.IAppService
{
    public interface IPersonAppService
    {
        Response InsertPerson(PersonDto personDto);
        ResponseType<PersonWithAdressDto> GetPersonById(int personId);
        Response UpdatePerson(PersonWithAdressDto personWithAdressDto);
        Response DeletePerson(int personId);
        PersonWithAdressDto SerarchByPersonAndCity(string personName, string city);
    }
}
