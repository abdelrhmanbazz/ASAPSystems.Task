using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.Core.Entity.Entities;
using ASAPSystems.Task.IApplication.IAppService;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

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
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
                return BadRequest(new { message = ex.Message });
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
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
                return BadRequest(new { message = ex.Message });
            }
            return Ok(new ResponseType<PersonWithAdressDto>()
            {
                MyObject = response.MyObject,
                HttpStatusCode = HttpStatusCode.OK,
                HttpResponseMessage = $"success"
            });
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        [Route("UpdatePerson")]
        public IActionResult UpdatePerson(PersonWithAdressDto personWithAdressDto)
        {
            Response response = null;
            try
            {
                response = _personAppService.UpdatePerson(personWithAdressDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
                return BadRequest(new { message = ex.Message });
            }
            return new ObjectResult(response.HttpResponseMessage)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("DeletePerson")]
        public IActionResult DeletePerson(int personId)
        {
            Response response = null;
            try
            {
                response = _personAppService.DeletePerson(personId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
                return BadRequest(new { message = ex.Message });
            }
            return new ObjectResult(response.HttpResponseMessage)
            {
                StatusCode = (int)response.HttpStatusCode
            };
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("SearchByPersonNameAndCity")]
        public IActionResult SearchByPersonNameAndCity(string personName, string City)
        {

            try
            {
                return Ok(_personAppService.SerarchByPersonAndCity(personName, City));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, MethodBase.GetCurrentMethod().Name);
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
