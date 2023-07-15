using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Common.DTOs
{
    public class ResponseType<T>
    {
        public T MyObject { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string HttpResponseMessage { get; set; }
    }
}
