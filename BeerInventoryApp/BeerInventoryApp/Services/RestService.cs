using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;
#if WINDOWS_UWP
using PortableRest;
#else
using RestSharp;
#endif

namespace BeerInventoryApp.Services
{
    public class GenericRestClient
    {
        RestClient client;

        public GenericRestClient(String baseUrl)
        {
#if WINDOWS_UWP
            client = new RestClient()
            {
                BaseUrl = baseUrl
            };
#else
            client = new RestClient(baseUrl);
#endif
        }

        public T Execute<T>(GenericRestRequest request)
#if WINDOWS_UWP
            where T : class
#else
            where T : new()
#endif
        {
#if WINDOWS_UWP
            return client.ExecuteAsync<T>(request.request).Result;
#else
            return client.Execute<T>(request.request).Data;
#endif
        }

        public void Post(GenericRestRequest request)
        {
            // Create an HTTP web request using the URL:
            var url = client.BaseUrl + request.GetResource();
            var uri = new Uri(url);
            var httpRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpRequest.ContentType = "application/json";
            httpRequest.Method = "POST";

            Debug.WriteLine("Posting to " + httpRequest.RequestUri);

            httpRequest.GetResponseAsync();
        }
    }

    public class GenericRestRequest
    {
        public RestRequest request { get; set; }

        public String resource { get; set; } = "";

        public List<Tuple<String, String>> parameters { get; set; } = new List<Tuple<String,String>>();

        public GenericRestRequest(String resource)
        {
            request = new RestRequest(resource);
            this.resource = resource;
        }

        public void AddQueryParameter(String key, String value)
        {
#if WINDOWS_UWP
            request.AddParameter(key, value);
            parameters.Add(new Tuple<string, string>(key, value));
#else
            request.AddQueryParameter(key, value);
            parameters.Add(new Tuple<string, string>(key, value));
#endif
        }

        public String GetResource()
        {
            var toReturn = resource;

            for (int x=0; x < parameters.Count; x++)
            {
                var param = parameters[x];
                if (x == 0)
                {
                    toReturn += "?" + Uri.EscapeDataString(param.Item1) + "=" + Uri.EscapeDataString(param.Item2);
                }
                else
                {
                    toReturn += "&" + Uri.EscapeDataString(param.Item1) + "=" + Uri.EscapeDataString(param.Item2);
                }
            }

            return toReturn;
        }
    }
}
