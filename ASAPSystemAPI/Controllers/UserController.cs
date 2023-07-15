using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.IApplication.IAppService;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace ASAPSystemAPI.Controllers
{
    public class UserController : Controller
    {
        #region PROP
        private readonly ILogger<UserController> _logger;
        private readonly IUserAppService _UserAppService;
        #endregion
        #region CTOR
        public UserController(ILogger<UserController> logger, IUserAppService UserAppService)
        {
            _logger = logger;
            _UserAppService = UserAppService;
        }
        #endregion
        [HttpPost]
        [Route("register")]
        public IActionResult register(UserDto userDto)
        {
            try
            {
                _UserAppService.Register(userDto);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {

            try
            {
               var token= _UserAppService.Login(userLoginDto);
                if(string.IsNullOrWhiteSpace(token))
                {
                    return Unauthorized();
                }
                return Ok(token);
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}
