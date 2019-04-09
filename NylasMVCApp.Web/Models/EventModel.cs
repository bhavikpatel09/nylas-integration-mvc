using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web.Models
{
    public class EventModel
    {
        public string account_id { get; set; }
        public bool busy { get; set; }
        public string calendar_id { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string location { get; set; }
        public string message_id { get; set; }
        public string owner { get; set; }
        public string read_only { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public When when { get; set; }
        public List<Participant> participants { get; set; }
    }
}