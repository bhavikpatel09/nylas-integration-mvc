using NylasMVCApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            HomeModel homeModel = new HomeModel();
            homeModel.Error = "";
            if(Session["token"] != null)
            {
                var model = Session["token"] as TokenResponse;
                homeModel.Token = model.access_token;
            }
            return View(homeModel);
        }
        [HttpPost]
        public async Task<ActionResult> Index(HomeModel homeModel)
        {
            if (homeModel != null && !string.IsNullOrEmpty(homeModel.Email))
            {
                var responseToken = FileService.GetTokenByEmail(homeModel.Email);
                if (responseToken != null)
                {
                    //using (var client = new HttpClient())
                    //{
                    //    client.BaseAddress = new Uri(Constants.ServiceConstants.Uri);

                    //    client.DefaultRequestHeaders.Clear();
                    //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    //    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseToken.access_token);
                    //    HttpResponseMessage response = await client.PostAsync("/oauth/revoke",null);

                    //    if (response.IsSuccessStatusCode)
                    //    {
                    //        var result = response.Content.ReadAsStringAsync().Result;

                            Session["token"] = responseToken;
                            homeModel.Error = "";
                            homeModel.Token = responseToken.access_token;
                            return RedirectToAction("Index", "Calendar");
                    //    }
                    //    else
                    //    {
                    //        homeModel.Error = "Invalid Email Address.";
                    //        var result = response.Content.ReadAsStringAsync().Result;
                    //        return View(homeModel);
                    //    }
                    //}
                }
            }
            homeModel.Error = "Enter Email Address.";
            return View(homeModel);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
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

                        string clientId = ConfigurationManager.AppSettings["ClientId"].ToString();//"7kvz9jl8a9eypd7p1assjj2wo";
                        string loginHint = ConfigurationManager.AppSettings["LoginHint"].ToString();
                        string redirectUri = ConfigurationManager.AppSettings["RedirectUri"].ToString();
                        string responseType = ConfigurationManager.AppSettings["ResponseType"].ToString();
                        string queryString = "?client_id=" + clientId + "&response_type=" + responseType + "&scope=email&login_hint=" + loginHint + "&redirect_uri=" + redirectUri + "&state";
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
        public ActionResult Logout()
        {
            Session.Clear();
            HomeModel homeModel = new HomeModel();
            homeModel.Email = "";
            homeModel.Error= "";
            homeModel.Token = "";
            return View("Index",homeModel);
        }
    }
}