using Newtonsoft.Json;
using NylasMVCApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NylasMVCApp.Web.Controllers
{
    public class CalendarController : Controller
    {
        public async Task<ActionResult> Index()
        {
            CalendarViewModel calendarViewModel = new CalendarViewModel();
            var tokenResponse = Session["token"] as TokenResponse;

            if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.access_token))
            {
                HttpResponseMessage response = await APIService.Get("/calendars", tokenResponse.access_token);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    calendarViewModel.Calendars = JsonConvert.DeserializeObject<List<CalendarDetail>>(result);
                    calendarViewModel.Error = "";
                    return View("Index", calendarViewModel);
                }
                else
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return Content(result);
                }
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(Constants.ServiceConstants.Uri);

                //    client.DefaultRequestHeaders.Clear();
                //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.access_token);

                //    HttpResponseMessage Res = await client.GetAsync("/calendars");

                //    if (Res.IsSuccessStatusCode)
                //    {
                //        var result = Res.Content.ReadAsStringAsync().Result;

                //        calendarViewModel.Calendars = JsonConvert.DeserializeObject<List<CalendarDetail>>(result);
                //        calendarViewModel.Error = "";
                //        return View("Index", calendarViewModel);
                //    }
                //    else
                //    {
                //        var result = Res.Content.ReadAsStringAsync().Result;
                //        return Content(result);
                //    }


                //}
            }
            calendarViewModel.Error = "Something is wrong... Please try re-login again!!!";
            return View("Index", calendarViewModel);
        }
        public ActionResult CalendarDetail(CalendarDetail calendarDetail)
        {
            if (calendarDetail != null)
            {
                return RedirectToAction("Index", "CalendarDetail", calendarDetail);
            }
            return View("Index");
        }
    }
}