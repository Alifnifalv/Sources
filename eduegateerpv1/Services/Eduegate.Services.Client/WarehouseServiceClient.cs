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

namespace Eduegate.Service.Client
{
    public class WarehouseServiceClient: BaseClient, IWarehouseServices
    {
        private static string ServiceHost { get { return new Domain.Setting.SettingBL().GetSettingValue<string>("ServiceHost"); } }
        private static string Service = string.Concat(ServiceHost, Eduegate.Framework.Helper.Constants.WAREHOUSE_SERVICE_NAME);

        public WarehouseServiceClient(CallContext callContext = null, Action<string> logger = null)
            : base(callContext, logger)
        {

        }

        public JobEntryHeadDTO CreateUpdateJobEntry(JobEntryHeadDTO jobEntry)
        {
            var uri = string.Format("{0}/CreateUpdateJobEntry", Service);
            return ServiceHelper.HttpPostGetRequest<JobEntryHeadDTO>(uri, jobEntry, _callContext, _logger);
        }

        public JobEntryHeadDTO GetJobEntryDetails(long jobEntryHeadIID)
        {
            var uri = string.Format("{0}/GetJobEntryDetails?jobEntryHeadIID={1}", Service, jobEntryHeadIID);
            return ServiceHelper.HttpGetRequest<JobEntryHeadDTO>(uri, _callContext, _logger);
        }

        public JobOperationHeadDTO GetJobOperationDetails(long jobEntryHeadIID)
        {
            var uri = string.Format("{0}/GetJobOperationDetails?jobEntryHeadIID={1}", Service, jobEntryHeadIID);
            return ServiceHelper.HttpGetRequest<JobOperationHeadDTO>(uri, _callContext, _logger);
        }

        public List<JobOperationHeadDTO> GetJobOperationDetailsByJobIds(string jobEntryHeadIIDs)
        {
            var uri = string.Format("{0}/GetJobOperationDetailsByJobIds?jobEntryHeadIIDs={1}", Service, jobEntryHeadIIDs);
            return ServiceHelper.HttpGetRequest<List<JobOperationHeadDTO>>(uri, _callContext, _logger);
        }

        public BasketDTO GetBasket(long basketID)
        {
            var uri = string.Format("{0}/GetBasket?basketID={1}", Service, basketID);
            return ServiceHelper.HttpGetRequest<BasketDTO>(uri, _callContext, _logger);
        }

        public BasketDTO SaveBasket(BasketDTO basketDTO)
        {
            var uri = string.Format("{0}/SaveBasket", Service);
            return ServiceHelper.HttpPostGetRequest<BasketDTO>(uri, basketDTO, _callContext, _logger);
        }

        public JobOperationHeadDTO AssignedJob(JobOperationHeadDTO jobOperation)
        {
            var uri = string.Format("{0}/AssignedJob", Service);
            var result = ServiceHelper.HttpPostRequest(uri, jobOperation, _callContext);
            return JsonConvert.DeserializeObject<JobOperationHeadDTO>(result);
        }

        public LocationDTO SaveSKULocation(long productSkuID, long validateLocationID, string verifyLocationBarcode)
        {
            var uri = string.Format("{0}/SaveSKULocation?productskuID={1}&validateLocationID={2}&verifyLocationBarcode={3}", Service, productSkuID, validateLocationID, verifyLocationBarcode);
            return ServiceHelper.HttpGetRequest<LocationDTO>(uri, _callContext, _logger);
        }

        public OrderContactMapDTO GetOrderShippingDetails(long IID, Services.Contracts.Enums.SearchView view)
        {
            var uri = string.Format("{0}/GetOrderShippingDetails?IID={1}&view={2}", Service, IID, view);
            return ServiceHelper.HttpGetRequest<OrderContactMapDTO>(uri, _callContext, _logger);
        }

        public bool DeleteJobEntry(long parentJobEntryHeadID, long jobEntryHeadID)
        {
            var uri = string.Format("{0}/DeleteJobEntry?parentJobEntryHeadID={1}&jobEntryHeadID={2}", Service, parentJobEntryHeadID, jobEntryHeadID);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public bool UpdateOrderContact(long ID, decimal Latitude, decimal Longitude)
        {
            var uri = string.Format("{0}/UpdateOrderContact?ID={1}&Latitude={2}&Longitude={3}", Service, ID, Latitude, Longitude);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public CustomerDTO GetCustomerDetailForJobMission(long transactionID)
        {
            var uri = string.Format("{0}GetCustomerDetailForJobMission?transactionID={1}", Service, transactionID);
            return ServiceHelper.HttpGetRequest<CustomerDTO>(uri, _callContext, _logger);
        }

        public ServiceProviderShipmentDetailDTO GetShipmentDetails(long jobEntryHeadIID)
        {
            var uri = string.Format("{0}/GetShipmentDetails?jobEntryHeadIID={1}", Service, jobEntryHeadIID);
            return ServiceHelper.HttpGetRequest<ServiceProviderShipmentDetailDTO>(uri, _callContext, _logger);
        }

        public bool ValidateSalesInvoiceForStockOut(long jobEntryHeadId, int documentRefTypeId)
        {
            var uri = string.Format("{0}/ValidateSalesInvoiceForStockOut?jobEntryHeadId={1}&documentRefTypeId={2}", 
                                    Service, jobEntryHeadId, documentRefTypeId);
            return ServiceHelper.HttpGetRequest<bool>(uri, _callContext, _logger);
        }

        public JobStatusDto UpdateJobStatus(long jobHeadId, byte jobOperationStatusId)
        {
            var uri = string.Format("{0}/UpdateJobStatus?jobHeadId={1}&jobOperationStatusId={2}",
                                   Service, jobHeadId, jobOperationStatusId);
            return ServiceHelper.HttpGetRequest<JobStatusDto>(uri, _callContext, _logger);
        }

        public JobEntryHeadDTO GetJobByHeadID(long headID)
        {
            var uri = string.Format("{0}/GetJobByHeadID?headID={1}", Service, headID);
            return ServiceHelper.HttpGetRequest<JobEntryHeadDTO>(uri, _callContext, _logger);
        }

        public JobEntryHeadDTO GetJobByJobHeadID(long headID)
        {
            var uri = string.Format("{0}/GetJobByJobHeadID?headID={1}", Service, headID);
            return ServiceHelper.HttpGetRequest<JobEntryHeadDTO>(uri, _callContext, _logger);
        }

        public JobEntryHeadDTO GetMissionByJobID(long jobID)
        {
            var uri = string.Format("{0}/GetMissionByJobID?jobID={1}", Service, jobID);
            return ServiceHelper.HttpGetRequest<JobEntryHeadDTO>(uri, _callContext, _logger);
        }


        public bool UpdateJobsStatus(List<JobEntryHeadDTO>jobs)
        { 
            var uri = string.Format("{0}/UpdateJobsStatus", Service); 
            var result = ServiceHelper.HttpPostRequest(uri, jobs, _callContext);
            return JsonConvert.DeserializeObject<bool>(result);
        }
    }
}
