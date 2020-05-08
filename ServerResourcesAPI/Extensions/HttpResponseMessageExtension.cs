using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerResourcesAPI.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static HttpResponseMessage CreateResponse<T>(this HttpRequestMessage requestMessage, HttpStatusCode statusCode, T content) 
        { 
            return new HttpResponseMessage() 
            { 
                StatusCode = statusCode, 
                Content = new StringContent(JsonConvert.SerializeObject(content)) 
            }; 
        }
    }
}
