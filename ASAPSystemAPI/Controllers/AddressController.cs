using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.IApplication.IAppService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASAPSystemAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        #region PROP
        private readonly ILogger<AddressController> _logger;
        private readonly IAddressAppService _addressAppService;
        #endregion
        #region CTOR
        public AddressController(ILogger<AddressController> logger, IAddressAppService addressAppService)
        {
            _logger = logger;
            _addressAppService = addressAppService;
        }
        #endregion
        [HttpPost]
        [Route("InsertAddress")]
        public IActionResult InsertAddress(AddressDto addressDto)
        {
            Response response = null;
            try
            {
                response = _addressAppService.InsertAddress(addressDto);
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
