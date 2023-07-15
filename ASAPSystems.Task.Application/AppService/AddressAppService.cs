using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.Core.Entity.Entities;
using ASAPSystems.Task.IApplication.IAppService;
using ASAPSystems.Task.Infrastructure.IEntityService.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Application.AppService
{
    public class AddressAppService : IAddressAppService
    {
        #region Properties
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ILogger<AddressAppService> _logger;
        private readonly IConfiguration configuration;
        #endregion
        #region CTORS :
        public AddressAppService(IUnitOfWork unitOfWork, ILogger<AddressAppService> logger, IConfiguration configuration)
        {
            _UnitOfWork = unitOfWork;
            this.configuration = configuration;
        }
        #endregion
        #region Methods
       public Response InsertAddress (AddressDto  addressDto)
        {
            Address address = new Address();
            Response httpStatusCodeWithMessageDTO = new Response()
            {
        
                HttpStatusCode = HttpStatusCode.InternalServerError,
                HttpResponseMessage = "Error Occured"
            };
            try
            {
                if (string.IsNullOrEmpty(addressDto.Country) && string.IsNullOrEmpty(addressDto.City) && string.IsNullOrEmpty(addressDto.Street))
                {
                    httpStatusCodeWithMessageDTO.HttpStatusCode = HttpStatusCode.BadRequest;
                    httpStatusCodeWithMessageDTO.HttpResponseMessage = "please enter all data  ";
                    return httpStatusCodeWithMessageDTO;
                }

                address.Country = addressDto.Country;
                address.City = addressDto.City;
                address.Street = addressDto.Street;
                address.zip = addressDto.zip;

                if (_UnitOfWork.Address.InsertAddress(address))
                {
                    if (_UnitOfWork.Commit() > 0)
                    {
                        httpStatusCodeWithMessageDTO.HttpStatusCode = HttpStatusCode.OK;
                        httpStatusCodeWithMessageDTO.HttpResponseMessage = "Inserted successfully";
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
            }
            return httpStatusCodeWithMessageDTO;
        }
        #endregion
    }
}
