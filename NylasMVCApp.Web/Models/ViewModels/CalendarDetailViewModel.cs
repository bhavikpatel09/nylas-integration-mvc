using NylasMVCApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web
{
    public class CalendarDetailViewModel
    {
        public CalendarDetail CalendarDetail { get; set; }
        public List<EventModel> Events { get; set; }
        public string Error { get; set; }

    }
}