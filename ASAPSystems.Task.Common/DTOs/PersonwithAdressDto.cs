using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Common.DTOs
{
    public class PersonWithAdressDto
    {
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public int Age { get; set; }
        public int AddressId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string zip { get; set; }
    }
}
