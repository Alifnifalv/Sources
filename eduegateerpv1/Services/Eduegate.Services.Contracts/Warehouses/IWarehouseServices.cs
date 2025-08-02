using System.Collections.Generic;
using Eduegate.Services.Contracts.ExternalServices;

namespace Eduegate.Services.Contracts.Warehouses
{
    public interface IWarehouseServices
    {
        JobEntryHeadDTO CreateUpdateJobEntry(JobEntryHeadDTO jobEntry);

        JobEntryHeadDTO GetJobEntryDetails(long jobEntryHeadIID);

        JobOperationHeadDTO GetJobOperationDetails(long jobEntryHeadIID);

        List<JobOperationHeadDTO> GetJobOperationDetailsByJobIds(string jobEntryHeadIIDs);

        BasketDTO GetBasket(long basketID);

        BasketDTO SaveBasket(BasketDTO basketDTO);

        JobOperationHeadDTO AssignedJob(JobOperationHeadDTO jobOperation);

        LocationDTO SaveSKULocation(long productSkuID, long validateLocationID, string verifyLocationBarcode);

        OrderContactMapDTO GetOrderShippingDetails(long IID, Eduegate.Services.Contracts.Enums.SearchView view);

        bool DeleteJobEntry(long parentJobEntryHeadID, long jobEntryHeadID);

        bool UpdateOrderContact(long ID, decimal Latitude, decimal Longitude);

        CustomerDTO GetCustomerDetailForJobMission(long transactionID);

        ServiceProviderShipmentDetailDTO GetShipmentDetails(long jobEntryHeadIID);

        bool ValidateSalesInvoiceForStockOut(long jobEntryHeadId, int documentRefTypeId);

        JobStatusDto UpdateJobStatus(long jobHeadId, byte jobOperationStatusId);

        JobEntryHeadDTO GetJobByHeadID(long headID);

        JobEntryHeadDTO GetJobByJobHeadID(long headID);

        JobEntryHeadDTO GetMissionByJobID(long jobID);

        bool UpdateJobsStatus(List<JobEntryHeadDTO> jobs); 
    }
}