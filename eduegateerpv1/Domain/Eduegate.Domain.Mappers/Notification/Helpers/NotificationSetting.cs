using Eduegate.Domain.Entity;
using Eduegate.Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Eduegate.Domain.Mappers.Notification.Helpers
{
    public class NotificationSetting
    {
        public static Dictionary<string,string> GetParentAppSettings()
        {
            var settings = new Dictionary<string, string>();

            settings.Add("KEY_FILE", "parent.json");

            //var clientInstance = GetSettingDetail("CLIENTINSTANCE");
            //settings.Add("KEY_FILE", clientInstance + "_parent.json");

            //settings.Add("CLIENTINSTANCE", clientInstance);
            //settings.Add("KEY_FILE", clientInstance + "_customer.json");
            //settings.Add("APP_NAME", new Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_APP_NAME"));            
            //settings.Add("SENDER_ID", new Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_SENDER_ID"));
            //settings.Add("SERVER_API_KEY", new Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_SERVER_API_KEY"));

            return settings;
        }

        public static Dictionary<string, string> GetParentAppSettingsForPortal()
        {
            var settings = new Dictionary<string, string>();

            var clientInstance = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("CLIENTINSTANCE", "eduegate");
            var fcmAppName = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_APP_NAME", null);
            var fcmSenderID = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_SENDER_ID", null);
            var fcmServerAPI = new Eduegate.Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_SERVER_API_KEY", null);

            settings.Add("CLIENTINSTANCE", clientInstance);
            settings.Add("APP_NAME", fcmAppName);
            settings.Add("SENDER_ID", fcmSenderID);
            settings.Add("SERVER_API_KEY", fcmServerAPI);


            //var clientInstance = GetSettingDetail("CLIENTINSTANCE");
            //settings.Add("APP_NAME", GetSettingDetail("YOUR_FCM_APP_NAME"));
            //settings.Add("SENDER_ID", GetSettingDetail("YOUR_FCM_SENDER_ID"));
            //settings.Add("SERVER_API_KEY", GetSettingDetail("YOUR_FCM_SERVER_API_KEY"));

            return settings;
        }

        public static Dictionary<string, string> GetEmployeeAppSettings()
        {
            var settings = new Dictionary<string, string>();

            settings.Add("KEY_FILE", "employee.json");

            //var clientInstance = GetSettingDetail("CLIENTINSTANCE");
            //settings.Add("KEY_FILE", clientInstance + "_employee.json");


            //settings.Add("CLIENTINSTANCE", clientInstance);
            //settings.Add("APP_NAME", new Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_EMP_APP_NAME"));
            //settings.Add("SENDER_ID", new Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_EMP_SENDER_ID"));
            //settings.Add("SERVER_API_KEY", new Domain.Setting.SettingBL(null).GetSettingValue<string>("YOUR_FCM_EMP_SERVER_API_KEY"));

            return settings;
        }

        public static string GetSettingDetail(string settingKey)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var settingDetail = (from setting in dbContext.Settings
                        where setting.SettingCode.Equals(settingKey)
                        select setting).AsNoTracking().FirstOrDefault();

                return settingDetail?.SettingValue ?? null;
            }
        }

    }
}
