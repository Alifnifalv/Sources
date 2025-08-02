using System.ServiceModel;
using Eduegate.Domain;
using Eduegate.Framework.Services;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Services.Warehouse
{
    public class WarehouseServices : BaseService, IWarehouseServices
    {
        public JobEntryHeadDTO CreateUpdateJobEntry(JobEntryHeadDTO jobEntryDTO)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + jobEntryDTO.ToString());
                return new WarehouseBL(CallContext).CreateUpdateJobEntry(jobEntryDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public JobEntryHeadDTO GetJobEntryDetails(long jobEntryHeadIID)
        {
            try
            {
                return new WarehouseBL(CallContext).GetJobEntryDetails(jobEntryHeadIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public JobOperationHeadDTO GetJobOperationDetails(long jobEntryHeadIID)
        {
            try
            {
                return new WarehouseBL(CallContext).GetJobOperationDetails(jobEntryHeadIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public List<JobOperationHeadDTO> GetJobOperationDetailsByJobIds(string jobEntryHeadIIDs)
        {
            try
            {
                return new WarehouseBL(CallContext).GetJobOperationDetailsByJobIds(jobEntryHeadIIDs.Split(',').ToList());
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }


        public BasketDTO GetBasket(long basketID)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + basketID.ToString());
                return new WarehouseBL(CallContext).GetBasket(basketID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public BasketDTO SaveBasket(BasketDTO basketDTO)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + basketDTO.ToString());
                return new WarehouseBL(CallContext).SaveBasket(basketDTO);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public JobOperationHeadDTO AssignedJob(JobOperationHeadDTO jobOperation)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + jobOperation);
                return new WarehouseBL(CallContext).AssignedJob(jobOperation);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public LocationDTO SaveSKULocation(long productSkuID, long validateLocationID, string verifyLocationBarcode)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + string.Concat(productSkuID + " " + validateLocationID + " " + verifyLocationBarcode));
                return new WarehouseBL(CallContext).SaveSKULocation(productSkuID, validateLocationID, verifyLocationBarcode);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public OrderContactMapDTO GetOrderShippingDetails(long IID, Eduegate.Services.Contracts.Enums.SearchView view)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + string.Concat(IID + " " + view.ToString()));
                return new WarehouseBL(CallContext).GetOrderShippingDetails(IID, view);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool DeleteJobEntry(long parentJobEntryHeadID, long jobEntryHeadID)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + string.Concat(parentJobEntryHeadID + jobEntryHeadID));
                return new WarehouseBL(CallContext).DeleteJobEntry(parentJobEntryHeadID, jobEntryHeadID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateOrderContact(long ID, decimal Latitude, decimal Longitude)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + string.Concat(ID + "  " + Latitude + "  " + Longitude));
                return new WarehouseBL(CallContext).UpdateOrderContact(ID, Latitude, Longitude);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public CustomerDTO GetCustomerDetailForJobMission(long transactionID)
        {
            if(transactionID > 0)
            {
                try
                {
                    Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + transactionID);
                    return new WarehouseBL(CallContext).GetCustomerDetailForJobMission(transactionID);
                }
                catch (Exception exception)
                {
                    Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                    throw new FaultException("Internal server, please check with your administrator");
                }
            }
            else
            {
                return new CustomerDTO();
            }
        }

        public Contracts.ExternalServices.ServiceProviderShipmentDetailDTO GetShipmentDetails(long jobEntryHeadIID)
        {
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + jobEntryHeadIID);
                return new WarehouseBL(CallContext).GetShipmentDetails(jobEntryHeadIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool ValidateSalesInvoiceForStockOut(long jobEntryHeadId, int documentRefTypeId)
        {
            return new WarehouseBL(CallContext).ValidateSalesInvoiceForStockOut(jobEntryHeadId, documentRefTypeId);
        }

        public JobStatusDto UpdateJobStatus(long jobHeadId, byte jobOperationStatusId)
        {
            return new WarehouseBL(CallContext).UpdateJobStatus(jobHeadId, jobOperationStatusId);
        }

        public JobEntryHeadDTO GetJobByHeadID(long headIID)
        {
            try
            {
                return new WarehouseBL(CallContext).GetJobByHeadID(headIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public JobEntryHeadDTO GetJobByJobHeadID(long headIID)
        {
            try
            {
                return new WarehouseBL(CallContext).GetJobByJobHeadID(headIID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public JobEntryHeadDTO GetMissionByJobID(long jobID)
        {
            try
            {
                return new WarehouseBL(CallContext).GetMissionByJobID(jobID);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }

        public bool UpdateJobsStatus(List<JobEntryHeadDTO> jobs)
        {  
            try
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Info("Service Result : " + jobs);
                return new WarehouseBL(CallContext).UpdateJobsStatus(jobs);
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<WarehouseServices>.Fatal(exception.Message, exception);
                throw new FaultException("Internal server, please check with your administrator");
            }
        }
    }
}
