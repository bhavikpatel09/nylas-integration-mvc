using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web.Models
{
    public class CalendarDetail
    {
        public string name { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string account_id { get; set; }
        public string read_only { get; set; }
    }
}