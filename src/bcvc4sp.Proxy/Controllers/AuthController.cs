﻿namespace Akumina.Proxy.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Cors;

    public sealed class AuthController : ApiController
    {
        // GET api/auth
        public HttpResponseMessage Get()
        {
            return GetRequest(Request);
        }

        // POST api/auth
        [HttpPost]
        public HttpResponseMessage Post()
        {
            return GetRequest(Request);
        }

        private HttpResponseMessage GetRequest(HttpRequestMessage request)
        {
            HttpResponseMessage result = request.CreateResponse();
            string requestUrl = request.RequestUri.AbsoluteUri;
            string requestPathAndQuery = request.RequestUri.PathAndQuery.Substring("/api/auth".Length);
            string redirectUri = "https://oauth.brightcove.com/v4/access_token";
            
            Uri redurectUri = new Uri(redirectUri);

            if (System.Web.HttpContext.Current.Request.ServerVariables["http_host"] != redurectUri.DnsSafeHost + ":" + redurectUri.Port)
            {

                string clientId = (System.Web.HttpContext.Current.Request.Form["client_id"] ?? "");
                string clientSecret = (System.Web.HttpContext.Current.Request.Form["client_secret"] ?? "");

                if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
                {
                    string auth_string = EncodeTo64(string.Format("{0}:{1}", clientId, clientSecret));

                    HttpRequestMessage forwardRequest = request.Clone(redirectUri);
                    
                    forwardRequest.Headers.Add("Authorization", "Basic " + auth_string);

                    HttpClient client = new HttpClient();
                    Task<HttpResponseMessage> response = client.SendAsync(forwardRequest);
                    Task.WaitAll(new Task[] { response });

                    var corsRequest = request.GetCorsRequestContext();
                    var corsOrigin = GlobalEnableCorsAttribute.AllowedCorsOrigin(corsRequest.Origin);
                    response.Result.Headers.Remove("access-control-allow-origin");

                    if (!String.IsNullOrWhiteSpace(corsOrigin))
                    {
                        response.Result.Headers.Add("access-control-allow-origin", corsOrigin);
                    }

                    if (request.Method == HttpMethod.Post)
                    {
                        string existing = response.Result.Content.ReadAsStringAsync().Result.ToString();
                        response.Result.Content = new StringContent(existing);
                        result = response.Result;
                        result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
                    }
                    else
                    {
                        result = response.Result;
                    }
                }
            }


            return result;
        }

        private string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.Encoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

    }

    public static class HttpRequestExt
    {
        public static HttpRequestMessage Clone(this HttpRequestMessage req, string newUri)
        {
            HttpRequestMessage clone = new HttpRequestMessage(req.Method, newUri)
            {
                Version = req.Version
            };

            if (req.Method != HttpMethod.Get)
            {
                clone.Content = req.Content;
            }

            foreach (KeyValuePair<string, object> prop in req.Properties)
            {
                clone.Properties.Add(prop);
            }

            foreach (KeyValuePair<string, IEnumerable<string>> header in req.Headers)
            {
                clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            clone.Headers.Host = new Uri(newUri).Authority;

            return clone;
        }
    }
}