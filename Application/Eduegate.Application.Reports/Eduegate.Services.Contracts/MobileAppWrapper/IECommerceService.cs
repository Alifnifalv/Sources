using Eduegate.Services.Contracts.Mutual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    [ServiceContract]
    public interface IECommerceService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShareHolderInfo?emiratesID={emiratesID}")]
        ShareHolderDTO GetShareHolderInfo(string emiratesID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveCustomerCard")]
        void SaveCustomerCard(CustomerCardDTO card);
    }
}
