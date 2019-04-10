using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NylasMVCApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace NylasMVCApp.Web
{
    public static class FileService
    {
        private static readonly string fileName = ConfigurationManager.AppSettings["AuthFileName"];
        public static void SaveToken(TokenResponse tokenResponse)
        {
            var authFile = HttpContext.Current.Server.MapPath("~/" + fileName);
            var tokenResponses = new List<TokenResponse>();
            if (File.Exists(authFile))
            {
                var json = File.ReadAllText(authFile);
                List<TokenResponse> tokens = JsonConvert.DeserializeObject<List<TokenResponse>>(json);
                if (tokens != null && tokens.Count > 0)
                {
                    tokenResponses = tokens;
                }
            }
            var existingToken = tokenResponses.Where(x => x.email_address.ToLower() == tokenResponse.email_address.ToLower().Trim()).FirstOrDefault();
            if (existingToken != null)
            {
                tokenResponses.Remove(existingToken);
            }
            tokenResponses.Add(tokenResponse);
            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(tokenResponses,
                               Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(authFile, newJsonResult);
        }
        public static TokenResponse GetTokenByEmail(string email)
        {
            var authFile = HttpContext.Current.Server.MapPath("~/" + fileName);
            var tokenResponses = new List<TokenResponse>();
            if (File.Exists(authFile))
            {
                var json = File.ReadAllText(authFile);
                List<TokenResponse> tokens = JsonConvert.DeserializeObject<List<TokenResponse>>(json);
                if (tokens != null && tokens.Count > 0)
                {
                    tokenResponses = tokens;
                    var token = tokenResponses.Where(x => x.email_address.ToLower() == email.ToLower().Trim()).FirstOrDefault();
                    return token;
                }
            }
            return null;
        }
    }
}