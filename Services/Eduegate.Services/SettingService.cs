using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Services
{
    public class SettingService : BaseService, ISettingService
    {
       
        public SettingDTO GetSettingDetail(string settingKey)
        {
            try
            {
                var settingDetail = new Domain.Setting.SettingBL(CallContext).GetSettingDetail(settingKey);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public SettingDTO GetUserSettingDetail(string settingKey)
        {
            try
            {
                var settingDetail = new Domain.Setting.SettingBL(CallContext).GetUserSettingDetail(settingKey);
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<SettingDTO> GetAllUserSettingDetail()
        {
            try
            {
                var settingDetail = new Domain.Setting.SettingBL(CallContext).GetAllUserSettingDetail();
                Eduegate.Logger.LogHelper<ProductDetail>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ProductDetail>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public SettingDTO GetSettingDetailByCompany(string settingKey, long companyID)
        {
            try
            {
                SettingDTO settingDetail = new Domain.Setting.SettingBL(CallContext).GetSettingDetail(settingKey, companyID);
                Eduegate.Logger.LogHelper<SettingDTO>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public SettingDTO GetSettingDetailByCompanySite(string settingKey, long companyID, long siteID)
        {
            try
            {
                SettingDTO settingDetail = new Domain.Setting.SettingBL(CallContext).GetSettingDetail(settingKey, companyID, siteID);
                Eduegate.Logger.LogHelper<SettingDTO>.Info("Service Result : " + settingDetail.ToString());
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<SettingDTO> GetAllSettings()
        {
            try
            {
                return new Domain.Setting.SettingBL(CallContext).GetAllSettings();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public SettingDTO GetSettingByType(SettingType type, string referenceID, string settingKey, int companyID, int siteID)
        {
            try
            {
                SettingDTO settingDetail = new Domain.Setting.SettingBL(CallContext).GetSettingByType(type, referenceID, settingKey, companyID, siteID);
                return settingDetail;
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public T GetSettingDetailByCompanyWithDefault<T>(string settingKey, long companyID, T value)
        {
            throw new NotImplementedException();
        }

        public SettingDTO SaveSetting(SettingDTO settingDTO)
        {
            try
            {
                return new Domain.Setting.SettingBL(CallContext).SaveSetting(settingDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<ViewDTO> GetViews()
        {
            try
            {
                return new Domain.Setting.SettingBL(CallContext).GetViews();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ViewDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public string GetSettingValueByKey(string settingKey)
        {
            try
            {
                return new Domain.Setting.SettingBL(CallContext).GetSettingValue<string>(settingKey);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<SettingDTO> GetAllSettingDatas()
        {
            try
            {
                return new Domain.Setting.SettingBL(CallContext).GetAllSettings();
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<SettingDTO> GetSettingByGroup(string groupName)
        {
            try
            {
                return new Domain.Setting.SettingBL(CallContext).GetSettingByGroup(groupName);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<SettingDTO>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

    }
}
