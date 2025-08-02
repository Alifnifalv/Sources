using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Settings;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGlobalSettingService" in both code and config file together.
    [ServiceContract]
    public interface ISettingService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSettingDetail?settingKey={settingKey}")]
        SettingDTO GetSettingDetail(string settingKey);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetUserSettingDetail?settingKey={settingKey}")]
        SettingDTO GetUserSettingDetail(string settingKey);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllUserSettingDetail")]
        List<SettingDTO> GetAllUserSettingDetail();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSettingDetailByCompany?settingKey={settingKey}&companyID={companyID}")]
        SettingDTO GetSettingDetailByCompany(string settingKey, long companyID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSettingDetailByCompanyWithDefault?settingKey={settingKey}&companyID={companyID}")]
        T GetSettingDetailByCompanyWithDefault<T>(string settingKey, long companyID, T value);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSettingDetailByCompanySite?settingKey={settingKey}&companyID={companyID}&siteID={siteID}")]
        SettingDTO GetSettingDetailByCompanySite(string settingKey, long companyID, long siteID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllSettings")]
        List<SettingDTO> GetAllSettings();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSettingByType?type={type}&referenceID={referenceID}&settingKey={settingKey}&companyID={companyID}&siteID={siteID}")]
        SettingDTO GetSettingByType(SettingType type, string referenceID, string settingKey, int companyID, int siteID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSetting")]
        SettingDTO SaveSetting(SettingDTO settingDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetViews")]
        List<ViewDTO> GetViews();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSettingValueByKey?settingKey={settingKey}")]
        string GetSettingValueByKey(string settingKey);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetAllSettingDatas")]
        List<SettingDTO> GetAllSettingDatas();

    }
}
