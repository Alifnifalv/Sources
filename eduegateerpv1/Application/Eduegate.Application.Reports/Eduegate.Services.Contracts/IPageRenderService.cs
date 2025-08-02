using Eduegate.Framework.Contracts.Common.PageRender;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Eduegate.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPageRenderService" in both code and config file together.
    [ServiceContract]
    public interface IPageRenderService
    {

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetPageInfo?pageID={pageID}&parameter={parameter}")]
        PageDTO GetPageInfo(long pageID, string parameter);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SavePage")]
        PageDTO SavePage(PageDTO dto);
    }
}
