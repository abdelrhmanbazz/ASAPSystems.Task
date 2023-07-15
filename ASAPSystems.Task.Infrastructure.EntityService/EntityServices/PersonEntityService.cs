using ASAPSystems.Task.Common.DTOs;
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
    public class PersonEntityService : BaseRepository<Person>, IPersonEntityService
    {
        #region Prop
        public APPDbContext _AppDbContext { get; }
        private ILogger _Logger { get; }
        #endregion
        #region CTORS :
        public PersonEntityService(APPDbContext appDbContext, ILogger Logger) : base(appDbContext, Logger)
        {
            _AppDbContext = appDbContext;
            _Logger = Logger;
        }
        #endregion
        #region Methods :
        public bool InsertPerson(Person Person)
        {
            bool isInserted = default(bool);
            try
            {
                isInserted = Insert(Person);
            }
            catch (Exception)
            {

                throw;
            }
            return isInserted;
        }
        public Person GetPersonById(int PersonId)
        {
            Person person = null;
            try
            {
                person = GetWhere(x => x.PersonId == PersonId, "Address").FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            return person;
        }
        public bool UpdatePerson(Person Person)
        {
            bool isUpdated = default(bool);
            try
            {
                isUpdated = Update(Person);
            }
            catch (Exception)
            {

                throw;
            }
            return isUpdated;
        }
        public bool DeletePerson(Person person)
        {
            bool isDeleted = default(bool);
            try
            {
                isDeleted = Delete(person);
            }
            catch (Exception)
            {

                throw;
            }
            return isDeleted;
        }
        public PersonWithAdressDto SearchByPersonNameAndCity(string personName, string City)
        {
            PersonWithAdressDto personWithAdressDto = new PersonWithAdressDto(); 
            try
            {
 
               Person person = GetWhere(x => x.PersonName == personName && x.Address.City == City && x.AddressId == x.Address.AddressId, "Address").FirstOrDefault();
                if (person != null)
                {
                    personWithAdressDto.PersonName = person.PersonName;
                    personWithAdressDto.Age = person.Age;
                    personWithAdressDto.Country = person.Address.Country;
                    personWithAdressDto.City = person.Address.City;
                    personWithAdressDto.Street = person.Address.Street;
                    personWithAdressDto.zip = person.Address.zip;
                }

            }
            catch (Exception rr)
            {

                throw;
            }
            return personWithAdressDto;
        }
     

        #endregion
    }
}
