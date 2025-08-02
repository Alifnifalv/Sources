using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBannerService" in both code and config file together.
    [ServiceContract]
    public interface IBannerService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBanners")]
        List<BannerMasterDTO> GetBanners();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBannersByType?bannerType={bannerType}&status={status}")]
        List<BannerMasterDTO> GetBannersByType(BannerTypes bannerType, BannerStatuses status);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBanner/{BannerID}/")]
        BannerMasterDTO GetBanner(string bannerID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveBanner")]
        BannerMasterDTO SaveBanner(BannerMasterDTO bannerDTO);
    }
}
