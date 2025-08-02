using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Eduegate.Services.Contracts.Search;

namespace Eduegate.Services.Contracts.CustomerService
{
    [ServiceContract]
    public interface IRepairOrderService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRepaidOrders?currentPag={currentPage}&pageSize={pageSize}&orderBy={orderBy}")]
        SearchResultDTO GetRepaidOrders(int currentPage, int pageSize, string orderBy);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomers?currentPag={currentPage}&pageSize={pageSize}&orderBy={orderBy}")]
        SearchResultDTO GetCustomers(int currentPage, int pageSize, string orderBy);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRepaidOrder?orderID={orderID}")]
        RepairOrderDTO GetRepaidOrder(int orderID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveRepairOrder")]
        RepairOrderDTO SaveRepairOrder(RepairOrderDTO orderDTO);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetRepairOrderSummary")]
        SearchResultDTO GetRepairOrderSummary();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetVehcileDetails?chasisNo={chasisNo}&regitrationNo={regitrationNo}")]
        RepairVehicleDTO GetVehcileDetails(string chasisNo, string regitrationNo);
    }
}
