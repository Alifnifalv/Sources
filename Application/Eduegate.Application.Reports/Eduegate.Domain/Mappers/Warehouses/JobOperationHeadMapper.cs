using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Domain.Repository.Payroll;
using Eduegate.Framework.Interfaces;
using Eduegate.Services.Contracts.Enums.Warehouses;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Framework;
using Eduegate.Domain.Repository;
using Eduegate.Framework.Enums;

namespace Eduegate.Domain.Mappers.Warehouses
{
    public class JobOperationHeadMapper : IDTOEntityMapper<JobOperationHeadDTO, JobEntryHead>
    {
        private CallContext _context;

        public static JobOperationHeadMapper Mapper(CallContext context)
        {
            var mapper = new JobOperationHeadMapper();
            mapper._context = context;
            return mapper;
        }

        public List<JobOperationHeadDTO> ToDTO(List<JobEntryHead> heads)
        {
            var dtos = new List<JobOperationHeadDTO>();

            foreach (var head in heads)
            {
                dtos.Add(ToDTO(head));
            }

            return dtos;
        }

        public JobOperationHeadDTO ToDTO(JobEntryHead jobEntryHead)
        {
            if (jobEntryHead.IsNull())
            {
                throw new Exception("jobEntryHead is null");
            }
            // get customer detail

            var customer = jobEntryHead.TransactionHeadID.HasValue ? new CustomerRepository().GetCustomerByTransactionHeadId(jobEntryHead.TransactionHeadID.Value) : null;

            // Get cityname from shipping address
            var cityName = string.Empty;

            if (jobEntryHead.TransactionHead.IsNotNull() && jobEntryHead.TransactionHead.OrderContactMaps.IsNotNull() && jobEntryHead.TransactionHead.OrderContactMaps.Where(o => o.IsShippingAddress == true).IsNotNull())
            {
                var addressList = jobEntryHead.TransactionHead.OrderContactMaps.Where(o => o.IsShippingAddress == true).ToList();
                if (addressList.IsNotNull() && addressList.Count > 0)
                {
                    var cityID = addressList.First().CityID;// From query, 
                    City city = cityID > 0 ? new MutualRepository().GetCity(cityID.Value) : new City();
                    cityName = city.CityName;
                }
            }

            var entitlementAmount = jobEntryHead.TransactionHead == null || jobEntryHead.TransactionHead.TransactionHeadEntitlementMaps == null 
                ? null : jobEntryHead.TransactionHead.TransactionHeadEntitlementMaps.Sum(a=> a.Amount).ToString();

            var jobEntryHeadDTO = new JobOperationHeadDTO()
            {
                JobEntryHeadIID = jobEntryHead.JobEntryHeadIID,
                BranchID = jobEntryHead.BranchID.HasValue ? jobEntryHead.BranchID.Value : 0,
                BranchName = jobEntryHead.BranchID.HasValue ? new ReferenceDataRepository().GetBranch(jobEntryHead.BranchID.Value, false).BranchName : string.Empty,
                DocumentTypeID = jobEntryHead.DocumentTypeID.HasValue ? jobEntryHead.DocumentTypeID.Value : 0,
                DocumentTypeName = jobEntryHead.DocumentTypeID.HasValue ? new MetadataRepository().GetDocumentType(long.Parse(jobEntryHead.DocumentTypeID.Value.ToString())).TransactionTypeName : string.Empty,
                JobNumber = jobEntryHead.JobNumber,
                JobDate = jobEntryHead.JobStartDate,
                Remarks = jobEntryHead.Remarks,
                DeliveryDate = jobEntryHead.TransactionHead.IsNotNull() ? jobEntryHead.TransactionHead.DeliveryDate : null,
                EmployeeID = jobEntryHead.EmployeeID,
                EmployeeName = jobEntryHead.EmployeeID.HasValue ? new EmployeeRepository().GetEmployeeName(jobEntryHead.EmployeeID.Value) : string.Empty,
                OperationTypes = (JobOperationTypes)Enum.Parse(typeof(JobOperationTypes), jobEntryHead.JobStatusID.Value.ToString()),
                JobPriorityID = jobEntryHead.PriorityID.HasValue ? int.Parse(jobEntryHead.PriorityID.ToString()) : (int?)null,
                JobPriority = jobEntryHead.PriorityID.HasValue ? new WarehouseRepository().GetJobPriority(jobEntryHead.PriorityID.Value).Description : string.Empty,
                ReferenceTransaction = jobEntryHead.TransactionHeadID,
                BasketID = jobEntryHead.BasketID,
                JobStatusID = jobEntryHead.JobStatusID,
                JobSizeID = jobEntryHead.JobSizeID,
                JobOperationStatusID = jobEntryHead.JobOperationStatusID,
                JobOperationStatus = jobEntryHead.JobOperationStatusID.HasValue ? new WarehouseRepository().GetJobOperation(jobEntryHead.JobOperationStatusID.Value).Description : string.Empty,
                ProcessStartDate = jobEntryHead.ProcessStartDate,
                ProcessEndDate = jobEntryHead.ProcessEndDate,
                IsCashCollected = jobEntryHead.IsCashCollected.IsNotNull() ? Convert.ToBoolean(jobEntryHead.IsCashCollected) : default(bool),
                CreatedBy = jobEntryHead.CreatedBy,
                CreatedDate = jobEntryHead.CreatedDate,
                UpdatedBy = jobEntryHead.UpdatedBy,
                UpdatedDate = jobEntryHead.UpdatedDate,
                TimeStamps = jobEntryHead.TimeStamps == null ? null : Convert.ToBase64String(jobEntryHead.TimeStamps),
                LoginID = jobEntryHead.EmployeeID > 0 ? Convert.ToInt64(new EmployeeRepository().GetEmployee((long)jobEntryHead.EmployeeID, false).LoginID) : (long)_context.LoginID,
                Customer = customer == null ? null : new Eduegate.Framework.Contracts.Common.KeyValueDTO() { Key = customer.CustomerIID.ToString(), Value = customer.FirstName + "" + customer.LastName },
                PacketNo = jobEntryHead.NoOfPacket > 0 ? (byte)jobEntryHead.NoOfPacket : (byte)0,
                ParentJobEntryHeadId = jobEntryHead.ParentJobEntryHeadId > 0 ? jobEntryHead.ParentJobEntryHeadId : null,
                TransactionHeadId = jobEntryHead.TransactionHead.IsNotNull() ? jobEntryHead.TransactionHead.HeadIID : (long?)null,
                ////ShoppingCartID = jobEntryHead.TransactionHeadID.IsNotNull() ? new UserServiceRepository().GetTransactionHeadShoppingCartMap(Convert.ToInt64(jobEntryHead.TransactionHeadID)).ShoppingCartID.ToString() : null,
                ShoppingCartID = jobEntryHead.TransactionHead.IsNotNull() && jobEntryHead.TransactionHead.TransactionHeadShoppingCartMaps.IsNotNull() && jobEntryHead.TransactionHead.TransactionHeadShoppingCartMaps.Count>0 ? jobEntryHead.TransactionHead.TransactionHeadShoppingCartMaps.First().ShoppingCartID.ToString() : null,
                CityName = cityName,
                Amount = entitlementAmount,
                TransactionNo = jobEntryHead.TransactionHead.IsNotNull() ? jobEntryHead.TransactionHead.TransactionNo : null,
                Reason = jobEntryHead.Reason,
            };

            Basket basket = new WarehouseRepository().GetBasketByJob(Convert.ToInt64(jobEntryHead.JobEntryHeadIID));

            if (basket.IsNotNull()) 
                jobEntryHeadDTO.BasketName = basket.BasketCode;

            var orderContact = new WarehouseRepository().GetOrderContact(Convert.ToInt32(jobEntryHead.TransactionHeadID));

            if (orderContact.IsNotNull())
            {
                jobEntryHeadDTO.LocationLatitude = orderContact.Latitude;
                jobEntryHeadDTO.LocationLongitude = orderContact.Longitude;
            }

            if (jobEntryHead.JobEntryDetails.IsNotNull() && jobEntryHead.JobEntryDetails.Count > 0)
            {
                jobEntryHeadDTO.Detail = new List<JobOperationDetailDTO>();
                var detailMapper = JobOperationDetailMapper.Mapper(_context);

                foreach (var jobEntryDetail in jobEntryHead.JobEntryDetails)
                {
                    // check is we have TransactionDetails or not
                    if (jobEntryDetail.IsNotNull() && jobEntryHead.HasProperty("TransactionHead") && jobEntryHead.TransactionHead.IsNotNull() && jobEntryHead.TransactionHead.TransactionDetails.IsNotNull() && jobEntryHead.TransactionHead.TransactionDetails.Count > 0)
                    {
                        var trasactionDetail = jobEntryHead.TransactionHead.TransactionDetails.Where(x => x.ProductSKUMapID == jobEntryDetail.ProductSKUID).FirstOrDefault();

                        if (trasactionDetail != null)
                        {
                            // Map Unit Price, assuming that it will be always same 
                            // temperoraly fixed, becuase we don't need this Price for Purchase Order
                            if (!jobEntryHead.TransactionHead.TransactionNo.Contains("PO"))
                            {
                                jobEntryDetail.UnitPrice = trasactionDetail.UnitPrice;
                            }

                            //// if transaction is PO then we have to get Sku Cost Price ... from ProductPriceListSKUMaps
                            //var docType = new ReferenceDataRepository().GetDocumentType(jobEntryHead.TransactionHead.DocumentTypeID.Value);
                            //if (docType.ReferenceTypeID == (int)DocumentReferenceTypes.PurchaseOrder)
                            //{
                            //    // ...
                            //    var productPriceListSKUMap = new PriceSettingsRepository().GetProductPriceListForSKU(trasactionDetail.ProductSKUMapID.Value).FirstOrDefault();

                            //    //jobEntryDetail.UnitPrice = productPriceListSKUMap.IsNotNull() ? productPriceListSKUMap.Cost : null;
                            //}
                        }

                    }

                    jobEntryHeadDTO.Detail.Add(detailMapper.ToDTO(jobEntryDetail, jobEntryHead.CompanyID.HasValue ? jobEntryHead.CompanyID.Value : _context.CompanyID.Value));
                }
            }

            return jobEntryHeadDTO;
        }

        public JobEntryHead ToEntity(JobOperationHeadDTO dto)
        {
            JobEntryHead jobEntryHead = new JobEntryHead();

            if (dto.IsNotNull())
            {
                jobEntryHead.JobEntryHeadIID = dto.JobEntryHeadIID;
                jobEntryHead.JobOperationStatusID = dto.JobOperationStatusID;
                jobEntryHead.EmployeeID = dto.EmployeeID;
                jobEntryHead.UpdatedDate = DateTime.Now;
                jobEntryHead.UpdatedBy = (int)_context.LoginID;
            }

            return jobEntryHead;
        }
    }
}
