using System.Collections.Generic;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGlobalSettingService" in both code and config file together.
    public interface ISettingService
    {
        SettingDTO GetSettingDetail(string settingKey);

        SettingDTO GetUserSettingDetail(string settingKey);

        List<SettingDTO> GetAllUserSettingDetail();

        SettingDTO GetSettingDetailByCompany(string settingKey, long companyID);

        T GetSettingDetailByCompanyWithDefault<T>(string settingKey, long companyID, T value);

        SettingDTO GetSettingDetailByCompanySite(string settingKey, long companyID, long siteID);

        List<SettingDTO> GetAllSettings();

        SettingDTO GetSettingByType(SettingType type, string referenceID, string settingKey, int companyID, int siteID);

        SettingDTO SaveSetting(SettingDTO settingDTO);

        List<ViewDTO> GetViews();

        string GetSettingValueByKey(string settingKey);

        List<SettingDTO> GetAllSettingDatas();

        List<SettingDTO> GetSettingByGroup(string groupName);


    }
}