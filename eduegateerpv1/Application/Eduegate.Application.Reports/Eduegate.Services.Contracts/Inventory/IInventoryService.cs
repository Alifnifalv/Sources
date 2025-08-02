using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
     [ServiceContract]
    public interface IInventoryService
    {
         [OperationContract]
         [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetStockVerification?headIID={headIID}")]
         StockVerificationDTO GetStockVerification(long headIID);

         [OperationContract]
         [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
             RequestFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveStockVerification")]
         StockVerificationDTO SaveStockVerification(StockVerificationDTO verifiedData);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetProductListsByBranchID?branchID={branchID}&date={date}")]
        List<StockVerificationMapDTO> GetProductListsByBranchID(long branchID,DateTime date);
    }
}
