using ASAPSystems.Task.Common.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.IApplication.IAppService
{
    public interface IUserAppService
    {
        bool Register(UserDto userDto);
        string Login(UserLoginDto userLoginDto);
    }
}
