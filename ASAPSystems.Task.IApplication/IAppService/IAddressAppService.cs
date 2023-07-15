using ASAPSystems.Task.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.IApplication.IAppService
{
    public interface IAddressAppService
    {
        Response InsertAddress(AddressDto addressDto);
    }
}
