using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Common.DTOs
{
    public class AddressDto
    {
       
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string zip { get; set; }
    }
}
