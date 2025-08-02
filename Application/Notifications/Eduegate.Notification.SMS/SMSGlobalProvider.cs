using System;
using System.Configuration;
using System.Net;
using System.Web;

namespace Eduegate.Notification.SMS.SMSGlobal
{
    public class SMSGlobalProvider
    {
        public static string SendOTP(string mobileNumber, string textMessage)
        {
            var userID = ConfigurationManager.AppSettings["SMSGlobalUserName"];
            var password = ConfigurationManager.AppSettings["SMSGlobalPassword"];
            var source = ConfigurationManager.AppSettings["SMSGlobalSource"];
            var api = ConfigurationManager.AppSettings["SMSGlobalAPI"];
            var smsTest = ConfigurationManager.AppSettings["SMSTest"];
            var otpNumber = GenerateOTP();
            var otp = string.Format(textMessage, otpNumber);
            if (string.IsNullOrEmpty(smsTest) || smsTest.ToLower() == "false")
            {
                var postData = SMSGlobalData(userID, password, source, mobileNumber, otp);
                SendSms(api, postData);
            }
            
            return otpNumber;
        }

        public static string GenerateOTP()
        {
            var chars1 = "1234567890";
            var stringChars1 = new char[4];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var str = new String(stringChars1);
            return str;
        }
        private static string SMSGlobalData(string username, string password, 
            string source, string destination, string text)
        {
            var postData = new System.Text.StringBuilder("action=sendsms");
            postData.Append("&user=");
            postData.Append(HttpUtility.UrlEncode(username));
            postData.Append("&password=");
            postData.Append(HttpUtility.UrlEncode(password));
            postData.Append("&from=");
            postData.Append(HttpUtility.UrlEncode(source));
            postData.Append("&to=");
            postData.Append(HttpUtility.UrlEncode(destination));
            postData.Append("&text=");
            postData.Append(HttpUtility.UrlEncode(text));
            return postData.ToString();
        }

        private static string SendSms(string url, string postData)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] data = encoding.GetBytes(postData);

            WebRequest smsRequest = System.Net.WebRequest.Create(url);
            smsRequest.Method = "POST";
            smsRequest.ContentType = "application/x-www-form-urlencoded";
            smsRequest.ContentLength = data.Length;

            System.IO.Stream smsDataStream = null;
            smsDataStream = smsRequest.GetRequestStream();
            smsDataStream.Write(data, 0, data.Length);
            smsDataStream.Close();

            var smsResponse = smsRequest.GetResponse();

            byte[] responseBuffer = new byte[smsResponse.ContentLength];
            smsResponse.GetResponseStream().Read(responseBuffer, 0, (int)smsResponse.ContentLength - 1);
            smsResponse.Close();

            return encoding.GetString(responseBuffer);
        }
    }
}
