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
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.School.AI;

namespace Eduegate.Domain.AI
{

    public class ChatBotBL
    {
        private CallContext Context { get; set; }

        public ChatBotBL(CallContext context)
        {
            Context = context;
        }

        public async Task<string> RasaChatAsync(ChatBotDTO request)
        {
            var AI_CHAT_BOT_URL = new Domain.Setting.SettingBL(null).GetSettingValue<string>("AI_CHAT_BOT_URL");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(AI_CHAT_BOT_URL);
                    HttpResponseMessage response;

                    if (!string.IsNullOrEmpty(request.VoiceData))
                    {
                        byte[] voiceBytes = Convert.FromBase64String(request.VoiceData);

                        using (var content = new MultipartFormDataContent())
                        {
                            var fileContent = new ByteArrayContent(voiceBytes);
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("audio/webm");

                            // ✅ Ensure the field name matches the AngularJS request
                            content.Add(fileContent, "audio", "voice_message.webm");

                            // ✅ Do not override 'Content-Type' for the request, let it be 'multipart/form-data'
                            response = await httpClient.PostAsync("chat", content);
                        }
                    }
                    else
                    {
                        var jsonContent = new StringContent(
                            Newtonsoft.Json.JsonConvert.SerializeObject(new { message = request.Message }),
                            Encoding.UTF8,
                            "application/json"
                        );

                        response = await httpClient.PostAsync("chat", jsonContent);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        return $"Rasa error: {response.StatusCode} - {errorResponse}";
                    }
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                Eduegate.Logger.LogHelper<string>.Fatal($"Chatbot interaction failed. Error: {errorMessage}", ex);
                return $"Chatbot interaction failed. Error: {errorMessage}";
            }
        }

    }
}