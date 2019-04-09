using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web.Models
{
    public class TokenRequestModel
    {
        public string client_Id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
        public string code { get; set; }


    }
}