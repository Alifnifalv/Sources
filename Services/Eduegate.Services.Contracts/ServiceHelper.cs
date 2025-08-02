using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Collections.Specialized;
using Eduegate.Framework.Logs;
using Newtonsoft.Json;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;

namespace Eduegate.Services.Contracts
{
    public static class ServiceHelper
    {
        public static string HttpPostRequest<T>(string requestUri, T postContent, CallContext context = null)
        {
            string postData = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(requestUri);
                webClient.Headers.Add(GetHeaderCollection(context));
                webClient.Encoding = Encoding.UTF8;
                string serviceResult = webClient.UploadString(uri, "POST", JsonConvert.SerializeObject(postContent));
                return serviceResult;

            }

        }

        public static T HttpPostGetRequest<T>(string requestUri, T postContent, CallContext context = null)
        {
            string postData = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(requestUri);
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add(GetHeaderCollection(context));
                string serviceResult = webClient.UploadString(uri, "POST", JsonConvert.SerializeObject(postContent));
                return JsonConvert.DeserializeObject<T>(serviceResult);
            }

        }

        public static string HttpPostSerializedObject(string requestUri, string postContent, CallContext context = null)
        {
            string postData = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(requestUri);
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add(GetHeaderCollection(context));
                string serviceResult = webClient.UploadString(uri, "POST", postContent);
                return serviceResult;
              
            }
        }

        public static string HttpGetRequest(string requestUri, CallContext context = null)
        {
            string returnValue = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                webClient.Headers.Add(GetHeaderCollection(context));
               returnValue = webClient.DownloadString(requestUri);
            }
            return returnValue;
        }

        private static NameValueCollection GetHeaderCollection(CallContext context)
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("Accept", "application/json;charset=UTF-8");
            collection.Add("Content-type", "application/json; charset=utf-8");
            if (context.IsNotNull())
                collection.Add("CallContext", JsonConvert.SerializeObject(context));
            return collection;
        }
    }
}