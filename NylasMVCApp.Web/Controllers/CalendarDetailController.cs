using Newtonsoft.Json;
using NylasMVCApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NylasMVCApp.Web.Controllers
{
    public class CalendarDetailController : Controller
    {
        // GET: CalendarDetail
        public async Task<ActionResult> Index(CalendarDetail calendarDetail)
        {
            if (calendarDetail != null)
            {
                CalendarDetailViewModel calendarDetailViewModel = new CalendarDetailViewModel();
                calendarDetailViewModel.CalendarDetail = calendarDetail;
                var tokenResponse = Session["token"] as TokenResponse;

                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    string queryString = "";
                    queryString = "?expand_recurring=false&show_cancelled=false&calendar_id=" + calendarDetail.id;
                    HttpResponseMessage response = await APIService.Get("/events", tokenResponse.access_token, queryString);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                                                
                        calendarDetailViewModel.Events = JsonConvert.DeserializeObject<List<EventModel>>(result);
                        calendarDetailViewModel.Error = "";
                        return View("Index", calendarDetailViewModel);
                    }
                    else
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        return Content(result);
                    }
                }
                calendarDetailViewModel.Error = "Something is wrong... Please try re-login again!!!";
                //return View("Index", calendarViewModel);
            }
            return View();
        }
        public ActionResult PostEvent(string calendarId)
        {
            return RedirectToAction("Index", "Event", new { calendarId = calendarId });
        }
    }
}