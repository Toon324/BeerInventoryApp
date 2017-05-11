using System;
using System.Collections.Generic;
using System.Text;
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
    }


    public class GenericRestRequest
    {
        public RestRequest request { get; set; }


        public GenericRestRequest(String resource)
        {
            request = new RestRequest(resource);
        }

        public void AddQueryParameter(String key, String value)
        {
#if WINDOWS_UWP
            request.AddParameter(key, value);
#else
            request.AddQueryParameter(key, value);
#endif
        }
    }
}
