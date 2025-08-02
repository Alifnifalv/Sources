using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Settings;
using Eduegate.Services;

namespace Eduegate.Service.Client.Direct
{
    public class SettingServiceClient : ISettingService
    {
        SettingService service = new SettingService();

        public SettingServiceClient(CallContext context = null, Action<string> logger = null)
        {
            service.CallContext = context;
        }

        public Services.Contracts.Settings.SettingDTO GetSettingDetail(string settingKey)
        {
            return service.GetSettingDetail(settingKey);
        }

        public List<Services.Contracts.Settings.SettingDTO> GetAllUserSettingDetail()
        {
            return service.GetAllUserSettingDetail();
        }

        public Services.Contracts.Settings.SettingDTO GetUserSettingDetail(string settingKey)
        {
            return service.GetUserSettingDetail(settingKey);
        }

        public Services.Contracts.Settings.SettingDTO GetSettingDetailByCompany(string settingKey, long companyID)
        {
            return service.GetSettingDetailByCompany(settingKey, companyID);
        }

        public Services.Contracts.Settings.SettingDTO GetSettingDetailByCompanySite(string settingKey, long companyID, long siteID)
        {
            return service.GetSettingDetailByCompanySite(settingKey, companyID, siteID);
        }

        public T GetSettingDetailByCompany<T>(string settingKey, long companyID)
        {
            var setting = GetSettingDetailByCompany(settingKey, companyID);

            try
            {
                return (T)Convert.ChangeType(setting.SettingValue, typeof(T));
            }
            catch (NullReferenceException)
            {
                return default(T);
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }

        public T GetSettingDetailByCompanyWithDefault<T>(string settingKey, long companyID, T value)
        {
            var setting = GetSettingDetailByCompany(settingKey, companyID);

            try
            {
                return (T)Convert.ChangeType(setting.SettingValue, typeof(T));
            }
            catch (NullReferenceException)
            {
                return value;
            }
            catch (InvalidCastException)
            {
                return value;
            }
        }

        public List<SettingDTO> GetAllSettings()
        {
            return service.GetAllSettings();
        }

        public SettingDTO GetSettingByType(Services.Contracts.Enums.SettingType type, string referenceID, string settingKey, int companyID, int siteID)
        {
            return service.GetSettingByType(type, referenceID, settingKey, companyID, siteID);
        }

        public SettingDTO SaveSetting(SettingDTO settingDTO)
        {
            return service.SaveSetting(settingDTO);
        }

        public List<ViewDTO> GetViews()
        {
            return service.GetViews();
        }

        public string GetSettingValueByKey(string settingKey)
        {
            return service.GetSettingValueByKey(settingKey);
        }

        public List<SettingDTO> GetAllSettingDatas()
        {
            return service.GetAllSettingDatas();
        }

        public List<SettingDTO> GetSettingByGroup(string groupName)
        {
            return service.GetSettingByGroup(groupName);
        }
        
    }
}
