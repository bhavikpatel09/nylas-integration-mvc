using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace NylasMVCApp.Web.Models
{
    public class APIResponseResult
    {
        public bool IsSuccess { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }
    }
}