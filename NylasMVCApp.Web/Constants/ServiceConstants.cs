using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web.Constants
{
    public static class ServiceConstants
    {
        public static string Uri = ConfigurationManager.AppSettings["ApiUri"].ToString();// "https://api.nylas.com";
    }
}