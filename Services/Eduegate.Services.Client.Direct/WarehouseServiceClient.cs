using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.ExternalServices;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Services.Warehouse;

namespace Eduegate.Service.Client.Direct
{
    public class WarehouseServiceClient: BaseClient, IWarehouseServices
    {
        WarehouseServices service = new WarehouseServices();

        public WarehouseServiceClient(CallContext callContext = null, Action<string> logger = null)
        {
            service.CallContext = callContext;
        }

        public JobEntryHeadDTO CreateUpdateJobEntry(JobEntryHeadDTO jobEntry)
        {
            return service.CreateUpdateJobEntry(jobEntry);
        }

        public JobEntryHeadDTO GetJobEntryDetails(long jobEntryHeadIID)
        {
            return service.GetJobEntryDetails(jobEntryHeadIID);
        }

        public JobOperationHeadDTO GetJobOperationDetails(long jobEntryHeadIID)
        {
            return service.GetJobOperationDetails(jobEntryHeadIID);
        }

        public List<JobOperationHeadDTO> GetJobOperationDetailsByJobIds(string jobEntryHeadIIDs)
        {
            return service.GetJobOperationDetailsByJobIds(jobEntryHeadIIDs);
        }

        public BasketDTO GetBasket(long basketID)
        {
            return service.GetBasket(basketID);
        }

        public BasketDTO SaveBasket(BasketDTO basketDTO)
        {
            return service.SaveBasket(basketDTO);
        }

        public JobOperationHeadDTO AssignedJob(JobOperationHeadDTO jobOperation)
        {
            return service.AssignedJob(jobOperation);
        }

        public LocationDTO SaveSKULocation(long productSkuID, long validateLocationID, string verifyLocationBarcode)
        {
            return service.SaveSKULocation(productSkuID, validateLocationID, verifyLocationBarcode);
        }

        public OrderContactMapDTO GetOrderShippingDetails(long IID, Services.Contracts.Enums.SearchView view)
        {
            return service.GetOrderShippingDetails(IID, view);
        }

        public bool DeleteJobEntry(long parentJobEntryHeadID, long jobEntryHeadID)
        {
            return service.DeleteJobEntry(parentJobEntryHeadID, jobEntryHeadID);
        }

        public bool UpdateOrderContact(long ID, decimal Latitude, decimal Longitude)
        {
            return service.UpdateOrderContact(ID, Latitude, Longitude);
        }

        public CustomerDTO GetCustomerDetailForJobMission(long transactionID)
        {
            return service.GetCustomerDetailForJobMission(transactionID);
        }

        public ServiceProviderShipmentDetailDTO GetShipmentDetails(long jobEntryHeadIID)
        {
            return service.GetShipmentDetails(jobEntryHeadIID);
        }

        public bool ValidateSalesInvoiceForStockOut(long jobEntryHeadId, int documentRefTypeId)
        {
            return service.ValidateSalesInvoiceForStockOut(jobEntryHeadId, documentRefTypeId);
        }

        public JobStatusDto UpdateJobStatus(long jobHeadId, byte jobOperationStatusId)
        {
            return service.UpdateJobStatus(jobHeadId, jobOperationStatusId);
        }

        public JobEntryHeadDTO GetJobByHeadID(long headID)
        {
            return service.GetJobByHeadID(headID);
        }

        public JobEntryHeadDTO GetJobByJobHeadID(long headID)
        {
            return service.GetJobByJobHeadID(headID);
        }

        public JobEntryHeadDTO GetMissionByJobID(long jobID)
        {
            return service.GetMissionByJobID(jobID);
        }


        public bool UpdateJobsStatus(List<JobEntryHeadDTO> jobs)
        {
            return service.UpdateJobsStatus(jobs);
        }
    }
}
