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
    public class DashboardController : Controller
    {
        public async Task<ActionResult> Index(string code)
        {
            if (Session["token"] == null)
            {
                if (!string.IsNullOrEmpty(code))
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Constants.ServiceConstants.Uri);

                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                        TokenRequestModel tokenRequestModel = new TokenRequestModel();
                        tokenRequestModel.code = code;
                        tokenRequestModel.client_secret = "b9lyjvnu234p2u09b9ycl2buq";
                        tokenRequestModel.client_Id = "7kvz9jl8a9eypd7p1assjj2wo";
                        tokenRequestModel.grant_type = "authorization_code";

                        List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();

                        pairs.Add(new KeyValuePair<string, string>("client_id", tokenRequestModel.client_Id));
                        pairs.Add(new KeyValuePair<string, string>("client_secret", tokenRequestModel.client_secret));
                        pairs.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                        pairs.Add(new KeyValuePair<string, string>("code", code));

                        FormUrlEncodedContent content = new FormUrlEncodedContent(pairs);
                        HttpResponseMessage Res = await client.PostAsync("/oauth/token", content);

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            /*
                             {
                              "access_token": "g7TekR3mDr9OqdUWi2MTI9Atp9J03H", 
                              "account_id": "bdpf0ep1b8xi077iwaebs3h48", 
                              "email_address": "bhavik.v.patel@gmail.com", 
                              "provider": "gmail", 
                              "token_type": "bearer"
                            }
                             */

                            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(result);
                            Session["token"] = tokenResponse;
                            ViewBag.Token = result;
                        }
                        else
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            return Content(result);
                        }

                        return View("Index");
                    }

                }
                else
                {
                    ViewBag.ErrorMessage = "Something went wrong!!! Please try again!!!";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "You have already logged-in and access taken has been set.";
                return View();
            }
        }
    }
}