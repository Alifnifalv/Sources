using Eduegate.Domain.Setting;
using Eduegate.Framework;
using Eduegate.Framework.Contracts.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Reflection;

namespace Eduegate.Domain.AI
{
    public class ScorePredictionBL
    {
        private CallContext Context { get; set; }

        public ScorePredictionBL(CallContext context)
        {
            Context = context;
        }

        public string GetScorePrediction(long studentID)
        {
            var AI_SCORE_PREDICTION_URL = new Domain.Setting.SettingBL(null).GetSettingValue<string>("AI_SCORE_PREDICTION_URL");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Set the base address for the API
                    httpClient.BaseAddress = new Uri(AI_SCORE_PREDICTION_URL);

                    // Construct the URL with the student ID parameter
                    var requestUrl = $"GetMarks?studentID={studentID}";

                    // Make the GET request synchronously
                    var response = httpClient.GetAsync(requestUrl).Result;

                    // Ensure the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content synchronously
                        var result = response.Content.ReadAsStringAsync().Result;

                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.Message.ToLower().Contains("inner") && ex.Message.ToLower().Contains("exception")
                    ? ex.InnerException?.Message : ex.Message;

                Eduegate.Logger.LogHelper<string>.Fatal($"Score prediction failed. Error message: {errorMessage}", ex);

                return null;
            }
        }

    }
}