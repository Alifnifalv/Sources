using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Eduegate.Services
{
    public static class ServiceHelper
    {
        public static string HttpPostRequest<T>(string requestUri, T postContent)
        {
            string postData = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(requestUri);
                webClient.Headers.Add(GetHeaderCollection());
                string serviceResult = webClient.UploadString(uri, "POST", JsonConvert.SerializeObject(postContent));
                return serviceResult;

                #region commented code
                /*
                DataContractJsonSerializer paymentRequestSerializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream memStream = new MemoryStream();
                paymentRequestSerializer.WriteObject(memStream, postContent);
                postData = Encoding.UTF8.GetString(memStream.ToArray(), 0, (int)memStream.Length);
                webClient.Headers.Add(GetHeaderCollection());
                webClient.Encoding = Encoding.UTF8;
                webClient.UploadString(requestUri, "POST", postData);
                 */
                #endregion
            }

        }

        public static string HttpPostSerializedObject(string requestUri, string postContent)
        {
            string postData = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                Uri uri = new Uri(requestUri);
                webClient.Headers.Add(GetHeaderCollection());
                string serviceResult = webClient.UploadString(uri, "POST", postContent);
                return serviceResult;
              
            }

        }

        public static string HttpGetRequest(string requestUri)
        {
            string returnValue = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                returnValue = webClient.DownloadString(requestUri);
            }
            return returnValue;
        }

        private static NameValueCollection GetHeaderCollection()
        {
            NameValueCollection collection = new NameValueCollection();
            collection.Add("Accept", "application/json");
            collection.Add("Content-type", "application/json");
            return collection;
        }
    }
}