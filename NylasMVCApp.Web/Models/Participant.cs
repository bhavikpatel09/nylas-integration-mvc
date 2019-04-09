using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web.Models
{
    public class Participant
    {
        public string comment { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string status { get; set; }
    }
}