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

namespace Eduegate.Service.Client
{
    public class SettingServiceClient : BaseClient, ISettingService
    {
        private static string serviceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string settingService = string.Concat(serviceHost, Eduegate.Framework.Helper.Constants.SETTING_SERVICE_NAME);

        public SettingServiceClient(CallContext context = null, Action<string> logger = null)
            : base(context, logger)
        {
        }

        public Services.Contracts.Settings.SettingDTO GetSettingDetail(string settingKey)
        {
            return ServiceHelper.HttpGetRequest<SettingDTO>(string.Concat(settingService, "GetSettingDetail?settingKey=", settingKey), _callContext, _logger);
        }

        public List<Services.Contracts.Settings.SettingDTO> GetAllUserSettingDetail()
        {
            return ServiceHelper.HttpGetRequest<List<SettingDTO>>(string.Concat(settingService, "GetAllUserSettingDetail"), _callContext, _logger);
        }

        public Services.Contracts.Settings.SettingDTO GetUserSettingDetail(string settingKey)
        {
            return ServiceHelper.HttpGetRequest<SettingDTO>(string.Concat(settingService, "GetUserSettingDetail?settingKey=", settingKey), _callContext, _logger);
        }

        public Services.Contracts.Settings.SettingDTO GetSettingDetailByCompany(string settingKey, long companyID)
        {
            var uri = string.Format("{0}/GetSettingDetailByCompany?settingKey={1}&companyID={2}", settingService, settingKey, companyID);
            return ServiceHelper.HttpGetRequest<SettingDTO>(uri, _callContext, _logger);
        }

        public Services.Contracts.Settings.SettingDTO GetSettingDetailByCompanySite(string settingKey, long companyID, long siteID)
        {
            var uri = string.Format("{0}/GetSettingDetailByCompanySite?settingKey={1}&companyID={2}&siteID={3}", settingService, settingKey, companyID, siteID);
            return ServiceHelper.HttpGetRequest<SettingDTO>(uri, _callContext, _logger);
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
            var uri = string.Format("{0}/GetAllSettings", settingService);
            return ServiceHelper.HttpGetRequest<List<SettingDTO>>(uri, _callContext, _logger);
        }

        public SettingDTO GetSettingByType(Services.Contracts.Enums.SettingType type, string referenceID, string settingKey, int companyID, int siteID)
        {
            var uri = string.Format("{0}/GetSettingByType?type={1}&referenceID={2}&settingKey={3}&companyID={4}&siteID={5}", settingService, type, referenceID, settingKey, companyID, siteID);
            return ServiceHelper.HttpGetRequest<SettingDTO>(uri, _callContext, _logger);
        }

        public SettingDTO SaveSetting(SettingDTO settingDTO)
        {
            return ServiceHelper.HttpPostGetRequest<SettingDTO>(string.Format("{0}/{1}", settingService, "SaveSetting"), settingDTO, _callContext, _logger);
        }

        public List<ViewDTO> GetViews()
        {
            var uri = string.Format("{0}/GetViews", settingService);
            return ServiceHelper.HttpGetRequest<List<ViewDTO>>(uri, _callContext, _logger);
        }

        public string GetSettingValueByKey(string settingKey)
        {
            throw new NotImplementedException();
        }

        public List<SettingDTO> GetAllSettingDatas()
        {
            throw new NotImplementedException();
        }

        public List<SettingDTO> GetSettingByGroup(string groupName)
        {
            throw new NotImplementedException();
        }
    }
}
