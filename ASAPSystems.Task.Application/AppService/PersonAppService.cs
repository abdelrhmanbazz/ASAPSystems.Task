using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.Core.Entity.Entities;
using ASAPSystems.Task.IApplication.IAppService;
using ASAPSystems.Task.Infrastructure.IEntityService.UnitOfWorks;
using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Application.AppService
{
    public class PersonAppService : IPersonAppService
    {
        #region Properties
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ILogger<PersonAppService> _logger;
        private readonly IConfiguration configuration;
        #endregion
        #region CTORS :
        public PersonAppService(IUnitOfWork unitOfWork, ILogger<PersonAppService> logger, IConfiguration configuration)
        {
            _UnitOfWork = unitOfWork;
            this.configuration = configuration;

        }
        #endregion
        #region Methods
        public Common.DTOs.Response InsertPerson(PersonDto personDto)
        {
            Person person = new Person();
            Common.DTOs.Response response = new Common.DTOs.Response()
            {

                HttpStatusCode = HttpStatusCode.InternalServerError,
                HttpResponseMessage = "Error Occured"
            };
            try
            {

                if (string.IsNullOrEmpty(personDto.PersonName) && personDto.Age == 0 && personDto.AddressId == 0)
                {
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.HttpResponseMessage = "please enter all data  ";
                    return response;
                }

                person.PersonName = personDto.PersonName;
                person.Age = personDto.Age;
                person.AddressId = personDto.AddressId;

                if (_UnitOfWork.Person.InsertPerson(person))
                {
                    if (_UnitOfWork.Commit() > 0)
                    {
                        response.HttpStatusCode = HttpStatusCode.OK;
                        response.HttpResponseMessage = "Inserted successfully";
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
            }
            return response;
        }
        public ResponseType<PersonWithAdressDto> GetPersonById(int personId)
        {
            Person person = null;
            PersonWithAdressDto personWithAdressDto = new PersonWithAdressDto();
            Common.DTOs.ResponseType<PersonWithAdressDto> response = new Common.DTOs.ResponseType<PersonWithAdressDto>()
            {
                MyObject = null,
                HttpStatusCode = HttpStatusCode.InternalServerError,
                HttpResponseMessage = "Error Occured"
            };
            try
            {
                if (personId == 0)
                {

                    return new ResponseType<PersonWithAdressDto>()
                    {
                        MyObject = null,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        HttpResponseMessage = "please enter person id"
                    };
                }

                person = _UnitOfWork.Person.GetPersonById(personId);
                if (person == null)
                {
                    return new ResponseType<PersonWithAdressDto>()
                    {
                        MyObject = null,
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        HttpResponseMessage = $" there is no person with this id : {personId}"
                    };
                }
                else
                {
                    personWithAdressDto.PersonId = person.PersonId;
                    personWithAdressDto.PersonName = person.PersonName;
                    personWithAdressDto.Age = person.Age;
                    personWithAdressDto.AddressId = person.AddressId;
                    personWithAdressDto.Country = person.Address.Country;
                    personWithAdressDto.Street = person.Address.Street;
                    personWithAdressDto.City = person.Address.City;
                    personWithAdressDto.zip = person.Address.zip;


                    return new ResponseType<PersonWithAdressDto>()
                    {
                        MyObject = personWithAdressDto,
                        HttpStatusCode = HttpStatusCode.OK,
                        HttpResponseMessage = $"success"
                    };

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
            }
            return response;
        }
        public Common.DTOs.Response DeletePerson(int personId)
        {
            Person person = new Person();
            Common.DTOs.Response response = new Common.DTOs.Response()
            {

                HttpStatusCode = HttpStatusCode.InternalServerError,
                HttpResponseMessage = "Error Occured"
            };
            try
            {

                if (personId == 0)
                {
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.HttpResponseMessage = "please enter valid id";
                    return response;
                }

                person = _UnitOfWork.Person.GetPersonById(personId);
                if (person != null)
                {

                if (_UnitOfWork.Person.DeletePerson(person))
                {
                    if (_UnitOfWork.Commit() > 0)
                    {
                        response.HttpStatusCode = HttpStatusCode.OK;
                        response.HttpResponseMessage = "Deleted successfully";
                    }
                }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
            }
            return response;
        }
        public Common.DTOs.Response UpdatePerson(PersonWithAdressDto personWithAdressDto)
        {
            Person person = new Person();
            Common.DTOs.Response response = new Common.DTOs.Response()
            {

                HttpStatusCode = HttpStatusCode.InternalServerError,
                HttpResponseMessage = "Error Occured"
            };
            try
            {

                if (string.IsNullOrEmpty(personWithAdressDto.PersonName) && personWithAdressDto.Age == 0 && personWithAdressDto.AddressId == 0)
                {
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.HttpResponseMessage = "please enter all data  ";
                    return response;
                }

                person.PersonId = personWithAdressDto.PersonId;
                person.PersonName = personWithAdressDto.PersonName;
                person.AddressId = personWithAdressDto.AddressId;
                person.Age = personWithAdressDto.Age;

                if (_UnitOfWork.Person.UpdatePerson(person))
                {
                    if (_UnitOfWork.Commit() > 0)
                    {
                        response.HttpStatusCode = HttpStatusCode.OK;
                        response.HttpResponseMessage = "Updated successfully";
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
            }
            return response;
        }
        public PersonWithAdressDto SerarchByPersonAndCity(string personName, string city)
        {
            PersonWithAdressDto personWithAdressDto = new PersonWithAdressDto();
            try
            {
                personWithAdressDto = _UnitOfWork.Person.SearchByPersonNameAndCity(personName, city);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
            }
            return personWithAdressDto;
        }
        #endregion
    }
}
