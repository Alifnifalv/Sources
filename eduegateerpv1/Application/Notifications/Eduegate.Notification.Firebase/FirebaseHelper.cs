using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eduegate.Notification.Firebase
{
    public class DeviceTokenInfo
    {
        public string ChannelName { get; set; }
        public string DeviceToken { get; set; }
    }

    public class FirebaseHelper
    {
        private string _instanceName;

        public FirebaseHelper(string keyFile)
        {
            _instanceName = keyFile;
            var clientInstance = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENTINSTANCE");

            if (keyFile == null || FirebaseApp.GetInstance(keyFile) == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "/NotificationHelpers/Clients/" + clientInstance,
                    string.IsNullOrEmpty(keyFile) ? "key.json" : keyFile)),
                }, keyFile == null ? "Eduegate" : keyFile);
            }
        }

        public async Task SendMessageAsync(string token, string title, string body, IReadOnlyDictionary<string, string> datas, string topic, string imageUrl = null)
        {
            try
            {
                var message = new Message()
                {
                    Data = datas,
                    Notification = new FirebaseAdmin.Messaging.Notification
                    {
                        Title = title,
                        Body = body,
                        ImageUrl = imageUrl,
                    },

                    Token = token.Length > 2 ? token.Substring(2, token.Length - 3) : token,
                };

                var messaging = FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance(string.IsNullOrEmpty(_instanceName) ? "Eduegate" : _instanceName));
                await messaging.SendAsync(message);
            }
            catch (Exception ex)
            {
                Logger.LogHelper<FirebaseHelper>.Fatal(ex.Message, ex);
            }
        }

        public async void SendMultiCastMessageAsync(List<DeviceTokenInfo> tokens, string title, string body, IReadOnlyDictionary<string, string> datas, string topic, string imageUrl = null)
        {
            try
            {
                var allTokens = tokens.Where(pair => !string.IsNullOrEmpty(pair.DeviceToken))
                       .Select(pair => new DeviceTokenInfo()
                       {
                           ChannelName = string.IsNullOrEmpty(pair.ChannelName) ? "Eduegate" : pair.ChannelName,
                           DeviceToken = (pair.DeviceToken.StartsWith("\"") && pair.DeviceToken.EndsWith("\"")) ? pair.DeviceToken.Substring(1, pair.DeviceToken.Length - 2) : pair.DeviceToken
                       }).ToList();

                var chunks = allTokens
                    .Select((pair, index) => new { Index = index, Key = pair.ChannelName, Value = pair.DeviceToken })
                    .GroupBy(x => new { x.Key, Chunk = x.Index / 450 })
                    .Select(group => group.Select(x => new KeyValuePair<string, string>(x.Key, x.Value)).ToList())
                    .ToList();

                foreach (var chunk in chunks)
                {
                    var message = new MulticastMessage()
                    {
                        Data = datas,
                        Notification = new FirebaseAdmin.Messaging.Notification()
                        {
                            Title = title,
                            Body = body,
                            ImageUrl = imageUrl,
                        },

                        Tokens = chunk.Select(x => x.Value).ToList(),

                        Android = new AndroidConfig()
                        {
                            Priority = Priority.High,
                            Notification = new AndroidNotification()
                            {
                                ChannelId = chunk.FirstOrDefault().Key,
                                Priority = NotificationPriority.HIGH,
                                NotificationCount = 1,
                                Visibility = NotificationVisibility.PUBLIC,
                                DefaultLightSettings = true
                            }
                        },
                        Apns = new ApnsConfig()
                        {
                            Aps = new Aps()
                            {
                                Alert = new ApsAlert
                                {
                                    Title = title,
                                    Body = body,
                                    LaunchImage = imageUrl
                                },
                                Sound = "default",
                            }
                        }
                    };

                    var messagingInstance = FirebaseApp.GetInstance(string.IsNullOrEmpty(_instanceName) ? "Eduegate" : _instanceName);
                    if (messagingInstance != null)
                    {
                        var messaging = FirebaseMessaging.GetMessaging(messagingInstance);
                        await messaging.SendEachForMulticastAsync(message);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogHelper<FirebaseHelper>.Fatal(ex.Message, ex);
            }
        }

    }
}