using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.ExternalServices
{
    [ServiceContract]
    public interface IServiceProviderAPIServices
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetTracking?referenceID={referenceID}")]
        string GetTracking(string referenceID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "AddShipment")]
        string AddShipment(ServiceProviderShipmentDetailDTO dto);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GenerateAWBPDF?referenceID={referenceID}")]
        byte[] GenerateAWBPDF(string referenceID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetSMSACities")]
        List<KeyValueDTO> GetSMSACities();
    }
}
