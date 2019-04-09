using NylasMVCApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web
{
    public class CalendarViewModel
    {
        public List<CalendarDetail> Calendars { get; set; }
        public string Error { get; set; }
    }
}