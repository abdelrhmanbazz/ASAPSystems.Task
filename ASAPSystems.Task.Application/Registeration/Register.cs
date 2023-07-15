using ASAPSystems.Task.Application.AppService;
using ASAPSystems.Task.IApplication.IAppService;
using ASAPSystems.Task.IApplication.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Application.Registeration
{
    public static class Register
    {
        public static void ResgisterServices(this IServiceCollection _Srv)
        {
            _Srv.AddScoped<IUserAppService, UserAppService>();
            _Srv.AddScoped<IPersonAppService, PersonAppService>();
            _Srv.AddScoped<IAddressAppService, AddressAppService>();

            //ASAPSystemAPISettings aSAPSystemAPISettings = new ASAPSystemAPISettings();
            //configuration.Bind("Settings", aSAPSystemAPISettings);
            //_Srv.AddSingleton(aSAPSystemAPISettings);




        }
    }
}
