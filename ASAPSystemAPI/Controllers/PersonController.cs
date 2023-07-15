using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.Core.Entity.Entities;
using ASAPSystems.Task.IApplication.IAppService;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ASAPSystemAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        #region PROP
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonAppService _personAppService;
        #endregion
        #region CTOR
        public PersonController(ILogger<PersonController> logger, IPersonAppService personAppService)
        {
            _logger = logger;
            _personAppService = personAppService;
        }
        #endregion
        [HttpPost]
        [Route("InsertPerson")]
        public IActionResult InsertPerson(PersonDto personDto)
        {
            Response response = null;
            try
            {
                response = _personAppService.InsertPerson(personDto);
            }
            catch (Exception)
            {

                throw;
            }
            return new ObjectResult(response.HttpResponseMessage)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
        [HttpGet]
        [Route("GetPersonById")]
        public IActionResult GetPersonById(int PersonId)
        {
            ResponseType<PersonWithAdressDto> response = new ResponseType<PersonWithAdressDto>();
            try
            {
                response = _personAppService.GetPersonById(PersonId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            return Ok(new ResponseType<PersonWithAdressDto>()
            {
                MyObject = response.MyObject,
                HttpStatusCode = HttpStatusCode.OK,
                HttpResponseMessage = $"success"
            });
        }
        [HttpPost]
        [Route("UpdatePerson")]
        public IActionResult UpdatePerson(PersonWithAdressDto personWithAdressDto)
        {
            Response response = null;
            try
            {
                response = _personAppService.UpdatePerson(personWithAdressDto);
            }
            catch (Exception)
            {

                throw;
            }
            return new ObjectResult(response.HttpResponseMessage)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
        [HttpPost]
        [Route("DeletePerson")]
        public IActionResult DeletePerson(int personId)
        {
            Response response = null;
            try
            {
                response = _personAppService.DeletePerson(personId);
            }
            catch (Exception)
            {

                throw;
            }
            return new ObjectResult(response.HttpResponseMessage)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
    }
}
