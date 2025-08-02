using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.CustomEntity;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Mappers;
using Eduegate.Domain.Repository;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Domain.Mappers.Warehouses;
using Eduegate.Services.Contracts.Payroll;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Services.Contracts;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Enums.Warehouses;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.ExternalServices;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Framework.Contracts.Common.Enums;

namespace Eduegate.Domain
{
    public class WarehouseBL
    {
        private CallContext _callContext;
        private static WarehouseRepository warehouseRepository = new WarehouseRepository();

        public WarehouseBL(CallContext context)
        {
            _callContext = context;
        }

        public JobEntryHeadDTO CreateUpdateJobEntry(JobEntryHeadDTO jobEntryDTO)
        {
            bool isUpdateTransactionNo = true;

            var jobEntryDetails = warehouseRepository.GetRemoveJobEntryDetail(JobEntryHeadMapper.Mapper(_callContext).ToEntity(jobEntryDTO));

            /*checking if document typed is changed or not 
            if it is changed then we have to update the Transaction Number with the last generated number*/
            if (jobEntryDTO.JobEntryHeadIID > 0)
            {
                var dbDTO = warehouseRepository.GetJobEntryDetails(jobEntryDTO.JobEntryHeadIID);
                isUpdateTransactionNo = dbDTO.DocumentTypeID != jobEntryDTO.DocumentTypeID ? true : false;
            }

            if (isUpdateTransactionNo)
            {
                jobEntryDTO.JobNumber = new MutualBL(_callContext).GetNextTransactionNumber(Convert.ToInt32(jobEntryDTO.DocumentTypeID));
            }

            var jobEntryHead = warehouseRepository.CreateUpdateJobEntry(JobEntryHeadMapper.Mapper(_callContext).ToEntity(jobEntryDTO)); //Calling repository method

            if (jobEntryHead.DocumentTypeID.HasValue)
            {
                var docType = new ReferenceDataRepository().GetDocumentType(jobEntryHead.DocumentTypeID.Value);
                // Update ParentJob
                if (jobEntryHead.IsNotNull() && jobEntryHead.ParentJobEntryHeadId.HasValue && jobEntryHead.ParentJobEntryHeadId > 0)
                {
                    if (docType.ReferenceTypeID == (int)DocumentReferenceTypes.FailedOperations)
                    {
                        // get the data based on ParentJobEntryId
                        var parentJobEntryHead = new WarehouseRepository().GetJobEntryDetails(jobEntryHead.ParentJobEntryHeadId.Value);
                        parentJobEntryHead.JobOperationStatusID = (byte)JobOperationStatuses.Completed;
                        warehouseRepository.CreateUpdateJobEntry(parentJobEntryHead);
                    }
                }


                // Save the comment for failed

                if (docType.ReferenceTypeID == (int)DocumentReferenceTypes.OutboundOperations &&
                    jobEntryHead.JobOperationStatusID == (byte)JobOperationStatuses.Failed)
                {
                    // Save Comment
                    InsertComment(jobEntryHead);
                }
            }

            if (jobEntryHead.IsNotNull() && jobEntryHead.DocumentTypeID.HasValue)
            {
                new MutualBL(_callContext).UpdateLastTransactionNo(Convert.ToInt32(jobEntryHead.DocumentTypeID), jobEntryHead.JobNumber);
            }

            if (jobEntryHead.JobEntryDetails.IsNotNull() && jobEntryHead.JobEntryDetails.Count > 0)
            {
                bool result = UpdateJobStatusOnCreateMission(jobEntryHead);
            }

            //ON Updating the job, deleting the missing job in job entry details
            if (jobEntryDetails.IsNotNull() && jobEntryDetails.Count > 0)
            {
                bool isDelete = warehouseRepository.DeleteJobEntry(jobEntryDetails);

                if (isDelete == true)
                    jobEntryHead = warehouseRepository.GetJobEntryDetails(Convert.ToInt32(jobEntryHead.JobEntryHeadIID));
            }

            // convert one object to another
            var jobEntryHead2 = JobEntryHeadMapper.Mapper(_callContext).Clone(jobEntryHead);
            if (jobEntryHead.DocumentTypeID.HasValue)
            {
                AddFailedOperationJobIfRequired(jobEntryHead2, _callContext);
            }

            return JobEntryHeadMapper.Mapper(_callContext).ToDTO(jobEntryHead);
        }



        private void AddFailedOperationJobIfRequired(JobEntryHead jobEntryHead, CallContext context)
        {
            var docType = new ReferenceDataRepository().GetDocumentType(jobEntryHead.DocumentTypeID.Value);

            if (docType.ReferenceTypeID == (int)DocumentReferenceTypes.OutboundOperations &&
                jobEntryHead.JobOperationStatusID == (byte)JobOperationStatuses.Failed)
            {
                //insert a failed job in the warehouse for receiving and make it available that for next mission
                jobEntryHead.ParentJobEntryHeadId = jobEntryHead.JobEntryHeadIID;
                jobEntryHead.JobEntryHeadIID = 0;
                jobEntryHead.EmployeeID = null;
                jobEntryHead.DocumentTypeID = int.Parse(new SettingRepository().GetSettingDetail("FAILEDJOBDOCUMENTTYPE", jobEntryHead.CompanyID.Value).SettingValue);
                jobEntryHead.JobOperationStatusID = (byte)JobOperationStatuses.NotStarted;
                jobEntryHead.JobStatusID = (int)JobStatuses.FailedReceiving;
                jobEntryHead.JobNumber = new MutualBL(_callContext).GetNextTransactionNumber(jobEntryHead.DocumentTypeID.Value);

                jobEntryHead.CreatedBy = int.Parse(context.LoginID.ToString());
                jobEntryHead.CreatedDate = DateTime.Now;
                jobEntryHead.UpdatedBy = int.Parse(context.LoginID.ToString());
                jobEntryHead.UpdatedDate = DateTime.Now;

                foreach (var detail in jobEntryHead.JobEntryDetails)
                {
                    detail.JobEntryDetailIID = 0;
                    detail.JobEntryHeadID = 0;
                    detail.CreatedBy = int.Parse(context.LoginID.ToString());
                    detail.CreatedDate = DateTime.Now;
                    detail.UpdatedBy = int.Parse(context.LoginID.ToString());
                    detail.UpdatedDate = DateTime.Now;
                }

                var resultJobEntryHead = warehouseRepository.CreateUpdateJobEntry(jobEntryHead); //Calling repository method
                new MutualBL(_callContext).UpdateLastTransactionNo(jobEntryHead.DocumentTypeID.Value, jobEntryHead.JobNumber);

                if (resultJobEntryHead != null && resultJobEntryHead.JobEntryHeadIID > 0)
                {
                    // Save Comment
                    InsertComment(resultJobEntryHead);
                }
            }
        }

        private void InsertComment(JobEntryHead resultJobEntryHead)
        {
            Comment comment = new Comment()
            {
                EntityTypeID = (short)EntityTypes.Job,
                ReferenceID = resultJobEntryHead.JobEntryHeadIID,
                Comment1 = "Failed and returning back to store.",
                CreatedDate = DateTime.Now,
                CreatedBy = _callContext.LoginID
            };

            new MutualRepository().SaveComment(comment);
        }

        private bool UpdateJobStatusOnCreateMission(JobEntryHead jobEntryHead)
        {
            bool result = false;
            List<JobEntryDetail> jobEntryDetails = jobEntryHead.JobEntryDetails.ToList();
            if (jobEntryDetails.IsNotNull() && jobEntryDetails.Count > 0)
            {
                foreach (JobEntryDetail jobEntryDetail in jobEntryDetails)
                {

                    if (jobEntryDetail.ParentJobEntryHeadID.IsNotNull())
                    {
                        JobEntryHead paentJobEntryHead = warehouseRepository.GetJobEntryDetails(Convert.ToInt64(jobEntryDetail.ParentJobEntryHeadID));

                        // while creating mission we are overrriide the parent employeeid which has assigned Mission
                        paentJobEntryHead.EmployeeID = jobEntryHead.EmployeeID;

                        if (paentJobEntryHead.IsNotNull())
                        {
                            if (paentJobEntryHead.JobStatusID == (int)JobStatuses.Packed || paentJobEntryHead.JobStatusID == (int)JobStatuses.FailedReceived || paentJobEntryHead.JobStatusID == (int)JobStatuses.ServiceJob)
                            {
                                paentJobEntryHead.JobStatusID = (int)JobStatuses.Assigned;
                                paentJobEntryHead.JobOperationStatusID = (int)JobOperationStatuses.NotStarted;
                                // to prevent A referential integrity constraint violation occurred (not right)
                                paentJobEntryHead.JobOperationStatus = null;
                                paentJobEntryHead.JobStatus = null;

                                result = warehouseRepository.UpdateJobStatusOnCreateMission(paentJobEntryHead);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public JobEntryHeadDTO GetJobEntryDetails(long jobEntryHeadIID)
        {
            var jobEntryHead = warehouseRepository.GetJobEntryDetails(jobEntryHeadIID);
            return JobEntryHeadMapper.Mapper(_callContext).ToDTO(jobEntryHead);
        }

        public JobOperationHeadDTO GetJobOperationDetails(long jobEntryHeadIID)
        {
            var jobEntryHead = warehouseRepository.GetJobEntryDetails(jobEntryHeadIID);
            return JobOperationHeadMapper.Mapper(_callContext).ToDTO(jobEntryHead);
        }

        public List<JobOperationHeadDTO> GetJobOperationDetailsByJobIds(List<string> jobEntryHeadIID)
        {
            var jobEntryHeads = warehouseRepository.GetJobOperationDetailsByJobIds(jobEntryHeadIID);
            return JobOperationHeadMapper.Mapper(_callContext).ToDTO(jobEntryHeads);
        }

        public BasketDTO GetBasket(long basketID)
        {
            var basketDetail = warehouseRepository.GetBasket(basketID);
            return BasketMapper.Mapper(_callContext).ToDTO(basketDetail);
        }



        public BasketDTO SaveBasket(BasketDTO basketDTO)
        {
            var basketDetail = warehouseRepository.SaveBasket(BasketMapper.Mapper(_callContext).ToEntity(basketDTO));
            return BasketMapper.Mapper(_callContext).ToDTO(basketDetail);
        }

        public JobOperationHeadDTO AssignedJob(JobOperationHeadDTO jobOperation)
        {
            if (jobOperation.IsPicked == true)
            {
                if (jobOperation.EmployeeID.IsNull())
                {
                    var employee = new EmployeeRepository().GetEmployeeByLoginID(Convert.ToInt64(_callContext.LoginID));

                    if (employee.IsNotNull())
                    {
                        jobOperation.EmployeeID = employee.EmployeeIID;
                    }
                }
            }

            JobEntryHead jobEntryHead = warehouseRepository.AssignedJob(JobOperationHeadMapper.Mapper(_callContext).ToEntity(jobOperation));
            return JobOperationHeadMapper.Mapper(_callContext).ToDTO(jobEntryHead);
        }

        public LocationDTO SaveSKULocation(long productSkuID, long validateLocationID, string verifyLocationBarcode)
        {
            Location location = new Location();
            location.ProductLocationMaps = new List<ProductLocationMap>();

            var locationDetail = new ProductCatalogRepository().GetLocationDetail(validateLocationID);

            if (locationDetail.IsNotNull())
            {
                location.LocationIID = locationDetail.LocationIID;
                location.BranchID = locationDetail.BranchID;
                location.Description = locationDetail.Description;
                location.LocationCode = locationDetail.LocationCode;
                location.LocationTypeID = locationDetail.LocationTypeID;
                location.Barcode = verifyLocationBarcode;
                location.CreatedBy = locationDetail.CreatedBy;
                location.CreatedDate = locationDetail.CreatedDate;
                location.UpdatedBy = (int)_callContext.LoginID;
                location.UpdatedDate = DateTime.Now;
                location.TimeStamps = locationDetail.TimeStamps;

                var productLocationMap = new ProductCatalogRepository().GetProductLocationMap(location.LocationIID);
                ProductLocationMap plm = productLocationMap;

                if (productLocationMap.IsNotNull())
                {
                    plm.ProductLocationMapIID = productLocationMap.ProductLocationMapIID;
                    plm.ProductID = productLocationMap.ProductID;
                    plm.ProductSKUMapID = productLocationMap.ProductSKUMapID;
                    plm.LocationID = productLocationMap.LocationID;
                    plm.CreatedBy = productLocationMap.CreatedBy;
                    plm.CreatedDate = productLocationMap.CreatedDate;
                    plm.UpdatedBy = (int)_callContext.LoginID;
                    plm.UpdatedDate = DateTime.Now;
                    plm.TimeStamps = productLocationMap.TimeStamps;

                    location.ProductLocationMaps.Add(plm);
                }
                else
                {
                    plm.ProductID = new ProductDetailRepository().GetProductSKUMap(productSkuID).IsNotNull() ? new ProductDetailRepository().GetProductSKUMap(productSkuID).ProductID : null;
                    plm.ProductSKUMapID = productSkuID;
                    plm.CreatedBy = (int)_callContext.LoginID;
                    plm.CreatedDate = DateTime.Now;
                    plm.UpdatedBy = (int)_callContext.LoginID;
                    plm.UpdatedDate = DateTime.Now;

                    location.ProductLocationMaps.Add(plm);
                }
            }
            else
            {
                location.LocationIID = location.LocationIID;
                location.Barcode = verifyLocationBarcode;
                location.CreatedBy = (int)_callContext.LoginID;
                location.CreatedDate = DateTime.Now;
                location.UpdatedBy = (int)_callContext.LoginID;
                location.UpdatedDate = DateTime.Now;

                ProductLocationMap plMapEntity = new ProductLocationMap();

                plMapEntity.ProductID = new ProductDetailRepository().GetProductSKUMap(productSkuID).IsNotNull() ? new ProductDetailRepository().GetProductSKUMap(productSkuID).ProductID : null;
                plMapEntity.ProductSKUMapID = productSkuID;
                plMapEntity.CreatedBy = (int)_callContext.LoginID;
                plMapEntity.CreatedDate = DateTime.Now;
                plMapEntity.UpdatedBy = (int)_callContext.LoginID;
                plMapEntity.UpdatedDate = DateTime.Now;

                location.ProductLocationMaps.Add(plMapEntity);
            }

            return Mappers.Warehouses.LocationMapper.Mapper(_callContext).ToDTO(new WarehouseRepository().SaveSKULocation(location));
        }

        public OrderContactMapDTO GetOrderShippingDetails(long IID, SearchView view)
        {
            var orderID = IID;
            switch (view)
            {
                case SearchView.JobEntryDetail:
                    var transaction = new TransactionRepository().GetTransaction(IID); //Passing the transactionHeadIID to fetch the sales Invoice info

                    if (transaction == null) return null;

                    // If sales invoice has sales order reference then we have to pass sales order headiid as orderID
                    if (transaction.ReferenceHeadID.IsNotNull() && transaction.ReferenceHeadID > 0)
                    {
                        orderID = (long)transaction.ReferenceHeadID;
                    }
                    else
                    {
                        orderID = transaction.HeadIID;
                    }

                    break;
                default:
                    break;
            }
            return Mappers.OrderContactMapMapper.Mapper(_callContext).ToDTO((new OrderRepository().GetOrderContacts(orderID).Where(x => x.IsShippingAddress == true).FirstOrDefault()));
        }

        public bool DeleteJobEntry(long parentJobEntryHeadID, long jobEntryHeadID)
        {
            bool result = false;

            List<JobEntryDetail> jobEntryDetails = warehouseRepository.GetJobDetailsByParentJobEntryHeadIID(parentJobEntryHeadID, jobEntryHeadID);

            if (jobEntryDetails.IsNotNull() && jobEntryDetails.Count > 0)
            {
                result = warehouseRepository.DeleteJobEntry(jobEntryDetails);
            }

            return result;
        }

        public bool UpdateOrderContact(long ID, decimal Latitude, decimal Longitude)
        {
            return warehouseRepository.UpdateOrderContact(ID, Latitude, Longitude);
        }

        public CustomerDTO GetCustomerDetailForJobMission(long transactionID)
        {
            return CustomerMapper.Mapper(_callContext).FromContactToCustomerDTO(warehouseRepository.GetCustomerDetailForJobMission(transactionID));
        }

        public ServiceProviderShipmentDetailDTO GetShipmentDetails(long jobEntryHeadIID)
        {
            var jobEntryHead = warehouseRepository.GetJobEntryDetails(jobEntryHeadIID);
            var orderDetails = new TransactionRepository().GetTransaction(jobEntryHead.TransactionHeadID.Value);
            var prodweight = jobEntryHead.JobEntryDetails.FirstOrDefault().ProductSKUMap.Product.Weight * jobEntryHead.JobEntryDetails.FirstOrDefault().Quantity;
            var contactDetail = orderDetails.OrderContactMaps.Where(a => a.IsShippingAddress.Value).FirstOrDefault();

            if (contactDetail.IsNull())
            {
                contactDetail = orderDetails.OrderContactMaps.FirstOrDefault();
            }

            decimal deliveryCharge = orderDetails.DeliveryCharge > 0 ? orderDetails.DeliveryCharge.Value : 0;
            var serviceProviderShipmentDetailDTO = new ServiceProviderShipmentDetailDTO()
            {
                Address = contactDetail.BuildingNo.GetEmptyIfNull() + "," + contactDetail.Floor.GetEmptyIfNull() + "," + contactDetail.Flat.GetEmptyIfNull() + "," +
                contactDetail.Block.GetEmptyIfNull() + "," + contactDetail.Street.GetEmptyIfNull() + "," + contactDetail.City.GetEmptyIfNull() + "," +
                contactDetail.State.GetEmptyIfNull(),
                //City = contactDetail.City,
                Mobile = contactDetail.MobileNo1,
                Telephone = contactDetail.TelephoneCode,
                CODAmount = orderDetails.TransactionDetails.Sum(a => a.Amount + deliveryCharge).ToString(),
                CustomerName = contactDetail.FirstName.GetEmptyIfNull() + ' ' + contactDetail.MiddleName.GetEmptyIfNull() + ' ' + contactDetail.LastName.GetEmptyIfNull(),
                ItemDescription = orderDetails.Description,
                NoOfPcs = "1",
                ReferenceNo = orderDetails.TransactionNo,
                Weight = Convert.ToString(prodweight),


            };
            serviceProviderShipmentDetailDTO.Address = serviceProviderShipmentDetailDTO.Address.TrimStart(',').TrimEnd(',');

            // Get City
            var city = new CityDTO();
            if (contactDetail.CityID > 0)
            {
                city = new MutualBL(_callContext).GetCity(contactDetail.CityID.Value);
                serviceProviderShipmentDetailDTO.City = city.CityName;
            }

            return serviceProviderShipmentDetailDTO;
            // }
            //var prod = jobEntryHead.TransactionHead.TransactionDetails.FirstOrDefault();
            //If contact details not saved as shipping, taking whatever available


            // remove unnecessary commas

        }

        public bool ValidateSalesInvoiceForStockOut(long jobEntryHeadId, int documentRefTypeId)
        {
            return new WarehouseRepository().ValidateSalesInvoiceForStockOut(jobEntryHeadId, documentRefTypeId);
        }

        public JobStatusDto UpdateJobStatus(long jobHeadId, byte jobOperationStatusId)
        {
            var jobStatusDto = new JobStatusDto();
            var jobStatus = new JobStatus();

            int jobStatusId;
            JobOperationStatuses opStatus = (JobOperationStatuses)jobOperationStatusId;
            switch (opStatus)
            {
                case JobOperationStatuses.OnHold:
                case JobOperationStatuses.ConfirmOrder:
                case JobOperationStatuses.RejectOrder:
                    jobStatusId = (int)JobStatuses.InProcess;
                    jobStatus = new WarehouseRepository().UpdateJobStatus(jobHeadId, jobOperationStatusId, jobStatusId);
                    jobStatusDto = JobStatusMapper.Mapper(_callContext).ToDTO(jobStatus);
                    break;
                case JobOperationStatuses.Deliveredtoshowroom:
                case JobOperationStatuses.Giventodriver:
                case JobOperationStatuses.DeliveredtoWarehouse:
                case JobOperationStatuses.DeliveredtoCustomer:
                    jobStatusId = (int)JobStatuses.InTransit;
                    jobStatus = new WarehouseRepository().UpdateJobStatus(jobHeadId, jobOperationStatusId, jobStatusId);
                    jobStatusDto = JobStatusMapper.Mapper(_callContext).ToDTO(jobStatus);
                    break;
                case JobOperationStatuses.ReceivedbyCustomer:
                case JobOperationStatuses.ReceivedatWarehouse:
                    jobStatusId = (int)JobStatuses.Receivedinwarehouse;
                    jobStatus = new WarehouseRepository().UpdateJobStatus(jobHeadId, jobOperationStatusId, jobStatusId);
                    jobStatusDto = JobStatusMapper.Mapper(_callContext).ToDTO(jobStatus);
                    break;
                default:
                    return null;
            }
            return jobStatusDto;
        }


        public JobEntryHeadDTO GetJobByHeadID(long HeadID)
        {
            return JobEntryHeadMapper.Mapper(_callContext).ToDTO(new MutualRepository().GetJobByHeadID(HeadID));
        }

        public JobEntryHeadDTO GetJobByJobHeadID(long HeadID)
        {
            return JobEntryHeadMapper.Mapper(_callContext).ToDTO(new MutualRepository().GetJobByJobHeadID(HeadID));
        }

        public JobEntryHeadDTO GetMissionByJobID(long jobID)
        {

            var mission = new MutualRepository().GetMissionByJobID(jobID);
            if (mission.JobEntryHeadIID.IsNotNull() && mission.JobEntryHeadIID > 0)
                return JobEntryHeadMapper.Mapper(_callContext).ToDTO(mission);
            else
                return null;
        }


        public bool UpdateJobsStatus(List<JobEntryHeadDTO> jobs)
        {
            List<JobEntryHead> entityList = FromJobEntryHeadsDTOtoEntity(jobs);
            var result = warehouseRepository.UpdateJobsStatus(entityList);
            return result;
        }

        public static List<JobEntryHead> FromJobEntryHeadsDTOtoEntity(List<JobEntryHeadDTO> JobEntryHeadDTOs)
        {  
            List<JobEntryHead> entityList = new List<JobEntryHead>(); 

            foreach(JobEntryHeadDTO dto in JobEntryHeadDTOs)
            {
                JobEntryHead entity = new JobEntryHead();
                entity.JobEntryHeadIID = dto.JobEntryHeadIID;
                entity.JobStatusID = dto.JobStatusID;
                entity.UpdatedBy = dto.UpdatedBy;
                entity.UpdatedDate = DateTime.Now;
                entityList.Add(entity);
            }
            return entityList; 
        }
    } 
}
