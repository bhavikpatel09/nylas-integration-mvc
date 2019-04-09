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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public async Task<ActionResult> Login()
        {
            try
            {
                var tokenResponse = Session["token"] as TokenResponse;
                if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.access_token))
                {
                    return RedirectToAction("Index", "Calendar");
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Constants.ServiceConstants.Uri);

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                        string clientId = "7kvz9jl8a9eypd7p1assjj2wo";
                        string queryString = "?client_id=" + clientId + "&response_type=code&scope=email&login_hint=bhavik.v.patel@gmail.com&redirect_uri=http://localhost:62256/Dashboard&state";
                        HttpResponseMessage Res = await client.GetAsync("/oauth/authorize" + queryString);

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            return Content(result);
                        }
                        else
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(result);
                            return Content(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var e = ex;
                return View("Index");
            }
        }
    }
}