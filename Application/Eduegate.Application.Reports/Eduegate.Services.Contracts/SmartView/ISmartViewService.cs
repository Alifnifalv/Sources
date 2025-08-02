using Eduegate.Services.Contracts.Enums;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts.SmartView
{
    [ServiceContract]
    public interface ISmartViewService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductTree?categoryID={categoryID}")]
        SmartTreeViewDTO GetProductTree(string categoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate
            = "GetSmartTreeView?type={type}&parentID={parentID}&searchText={searchText}")]
        SmartTreeViewDTO GetSmartTreeView(SmartViewType type, long? parentID, string searchText);
    }
}
