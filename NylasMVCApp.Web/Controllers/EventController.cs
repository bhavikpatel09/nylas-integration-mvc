using Newtonsoft.Json;
using NylasMVCApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NylasMVCApp.Web.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index(string calendarId)
        {
            EventModel eventModel = new EventModel();
            ViewBag.Success = "";
            eventModel.calendar_id = calendarId;
            if (!string.IsNullOrEmpty(calendarId))
            {
                ViewBag.Error = "";
            }
            else
            {
                ViewBag.Error = "Please select calendar to add a event";
            }
            return View(eventModel);
        }
        [HttpPost]
        public async Task<ActionResult> Index(EventModel eventModel)
        {
            if (eventModel != null)
            {
                var tokenResponse = Session["token"] as TokenResponse;

                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    eventModel.when = new When() { date = "2019-04-09" };

                    var objectData = JsonConvert.SerializeObject(eventModel);
                    var content = new StringContent(objectData.ToString(), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await APIService.Post("/events", tokenResponse.access_token, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        var newEventModel = new EventModel();
                        newEventModel.calendar_id = eventModel.calendar_id;
                        ViewBag.Success = "Event Added.";
                        return View(newEventModel);
                        //return RedirectToAction("Index", "CalendarDetail", new { calendarId = eventModel.calendar_id });
                    }
                    else
                    {
                        ViewBag.Success = "";
                        var result = response.Content.ReadAsStringAsync().Result;
                        return Content(result);
                    }
                }
            }
            return View(eventModel);

        }
    }
}