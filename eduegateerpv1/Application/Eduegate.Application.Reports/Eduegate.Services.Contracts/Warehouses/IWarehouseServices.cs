using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.ExternalServices;

namespace Eduegate.Services.Contracts.Warehouses
{
    [ServiceContract]
    public interface IWarehouseServices
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "CreateUpdateJobEntry")]
        JobEntryHeadDTO CreateUpdateJobEntry(JobEntryHeadDTO jobEntry);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobEntryDetails?jobEntryHeadIID={jobEntryHeadIID}")]
        JobEntryHeadDTO GetJobEntryDetails(long jobEntryHeadIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobOperationDetails?jobEntryHeadIID={jobEntryHeadIID}")]
        JobOperationHeadDTO GetJobOperationDetails(long jobEntryHeadIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobOperationDetailsByJobIds?jobEntryHeadIIDs={jobEntryHeadIIDs}")]
        List<JobOperationHeadDTO> GetJobOperationDetailsByJobIds(string jobEntryHeadIIDs);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetBasket?basketID={basketID}")]
        BasketDTO GetBasket(long basketID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveBasket")]
        BasketDTO SaveBasket(BasketDTO basketDTO);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "AssignedJob")]
        JobOperationHeadDTO AssignedJob(JobOperationHeadDTO jobOperation);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "SaveSKULocation?productskuID={productskuID}&validateLocationID={validateLocationID}&verifyLocationBarcode={verifyLocationBarcode}")]
        LocationDTO SaveSKULocation(long productSkuID, long validateLocationID, string verifyLocationBarcode);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetOrderShippingDetails?IID={IID}&view={view}")]
        OrderContactMapDTO GetOrderShippingDetails(long IID, Eduegate.Services.Contracts.Enums.SearchView view);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "DeleteJobEntry?parentJobEntryHeadID={parentJobEntryHeadID}&jobEntryHeadID={jobEntryHeadID}")]
        bool DeleteJobEntry(long parentJobEntryHeadID, long jobEntryHeadID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateOrderContact?ID={ID}&Latitude={Latitude}&Longitude={Longitude}")]
        bool UpdateOrderContact(long ID, decimal Latitude, decimal Longitude);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetCustomerDetailForJobMission?transactionID={transactionID}")]
        CustomerDTO GetCustomerDetailForJobMission(long transactionID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetShipmentDetails?jobEntryHeadIID={jobEntryHeadIID}")]
        ServiceProviderShipmentDetailDTO GetShipmentDetails(long jobEntryHeadIID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ValidateSalesInvoiceForStockOut?jobEntryHeadId={jobEntryHeadId}&documentRefTypeId={documentRefTypeId}")]
        bool ValidateSalesInvoiceForStockOut(long jobEntryHeadId, int documentRefTypeId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateJobStatus?jobHeadId={jobHeadId}&jobOperationStatusId={jobOperationStatusId}")]
        JobStatusDto UpdateJobStatus(long jobHeadId, byte jobOperationStatusId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobByHeadID?headID={headID}")]
        JobEntryHeadDTO GetJobByHeadID(long headID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetJobByJobHeadID?headID={headID}")]
        JobEntryHeadDTO GetJobByJobHeadID(long headID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "GetMissionByJobID?jobID={jobID}")]
        JobEntryHeadDTO GetMissionByJobID(long jobID);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "UpdateJobsStatus")]
        bool UpdateJobsStatus(List<JobEntryHeadDTO>jobs); 
    }
}
