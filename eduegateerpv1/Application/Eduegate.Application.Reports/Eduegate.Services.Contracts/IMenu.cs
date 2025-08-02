using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.MenuLinks;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IChat" in both code and config file together.
    [ServiceContract]
    public interface IMenu
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMenuDetails")]
        List<MenuDTO> GetMenuDetails();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetERPMenu?menuLinkType={menuLinkType}")]
        List<MenuDTO> GetERPMenu(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMenuDetailsByType?menuLinkType={menuLinkType}&siteID={siteID}")]
        List<MenuDTO> GetMenuDetailsByType(Eduegate.Services.Contracts.Enums.MenuLinkTypes menuLinkType, long siteID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMenuHierarchyByCategoryCode?siteID={siteID}&categoryCode={categoryCode}")]
        List<MenuDTO> GetMenuHierarchyByCategoryCode(int siteID, string categoryCode);
    }
}
