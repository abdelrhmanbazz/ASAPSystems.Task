using ASAPSystems.Task.IApplication.IAppService;
using Microsoft.Extensions.Logging;
using ASAPSystems.Task.Infrastructure.IEntityService.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASAPSystems.Task.Common.DTOs;
using ASAPSystems.Task.Core.Entity.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace ASAPSystems.Task.Application.AppService
{
    public class UserAppService :IUserAppService
    {
        #region Properties
        private readonly IUnitOfWork _UnitOfWork;
        private readonly ILogger<UserAppService> _logger;
        private readonly IConfiguration configuration;
        #endregion
        #region CTORS :
        public UserAppService(IUnitOfWork unitOfWork, ILogger<UserAppService> logger, IConfiguration configuration)
        {
            _UnitOfWork = unitOfWork;
            this.configuration = configuration;
  
        }
        #endregion
        #region Methods
        public bool Register(UserDto userDto)
        {
            bool isInserted = default(bool);
            User user = new User();

            try
            {
                if (!(string.IsNullOrEmpty(userDto.UserName) && string.IsNullOrEmpty(userDto.Password)))
                {
                    user.UserName = userDto.UserName;
                    user.Password = userDto.Password;
                    user.Role = userDto.Role;

                    isInserted = _UnitOfWork.User.AddUser(user);
                    _UnitOfWork.Commit();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return isInserted;
        }
        public string Login(UserLoginDto userLoginDto)
        {
            try
            {
                if (!(string.IsNullOrEmpty(userLoginDto.UserName) && string.IsNullOrEmpty(userLoginDto.Password)))
                {
                    User user = _UnitOfWork.User.GetUserByCredential(userLoginDto.UserName, userLoginDto.Password);
                    if (user != null)
                    {
                        var issuer = configuration["Jwt:Issuer"];
                        var audience = configuration["Jwt:Audience"];
                        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[]
                            {
                                new Claim("Id", user.UserId.ToString()),
                                new Claim(ClaimTypes.Role, user.Role),
                                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                            }),
                            Expires = DateTime.UtcNow.AddHours(2),
                            Issuer = issuer,
                            Audience = audience,
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);
                        var stringToken = tokenHandler.WriteToken(token);
                        return stringToken;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return string.Empty;
        }
        #endregion
    }
}
