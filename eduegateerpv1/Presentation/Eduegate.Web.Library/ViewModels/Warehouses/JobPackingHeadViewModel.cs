using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Web.Library.Enums;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    [Order(1)]
    public class JobPackingHeadViewModel : JobOperationHeadViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [DisplayName("No of Packet")]
        [LookUp("LookUps.Packet")]
        [Order(18)]
        public string PacketNo { get; set; }


        public static JobEntryHeadDTO ToJobEntryDTOFromJobPickingHeadAndDetailModel(JobPackingHeadViewModel packingHeadModel, List<JobPackingDetailViewModel> packingDetails)
        {
            JobEntryHeadDTO jobEntryDTO = new JobEntryHeadDTO();
            jobEntryDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

            if (packingHeadModel.IsNotNull() && packingDetails.Count > 0)
            {
                if (packingHeadModel.AssignBackEmployee.IsNotNull())
                    jobEntryDTO.EmployeeID = Convert.ToInt32(packingHeadModel.AssignBackEmployee.Key);
                else
                    jobEntryDTO.EmployeeID = packingHeadModel.EmployeeID;
                if (packingHeadModel.IsDoneFlag == true)
                {
                    if (packingHeadModel.JobOperationStatus.Key == Convert.ToString((int)JobOperationStatusTypes.InProcess))
                    {
                        
                        packingHeadModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.Completed);
                        packingHeadModel.JobStatusID = (int)JobOperationTypes.StockOut;
                        jobEntryDTO.EmployeeID = null;
                    }
                }

                jobEntryDTO.JobEntryHeadIID = packingHeadModel.TransactionHeadIID;
                jobEntryDTO.BranchID = packingHeadModel.BranchID > 0 ? Convert.ToInt32(packingHeadModel.BranchID) : (long?)null;
                jobEntryDTO.DocumentTypeID = packingHeadModel.DocumentTypeID > 0 ? Convert.ToInt32(packingHeadModel.DocumentTypeID) : (int?)null;
                jobEntryDTO.JobNumber = packingHeadModel.JobNumber;
                jobEntryDTO.JobStartDate = packingHeadModel.JobDate != null ? Convert.ToDateTime(packingHeadModel.JobDate) : (DateTime?)null;
                jobEntryDTO.Remarks = packingHeadModel.Remarks;
                jobEntryDTO.JobEndDate = packingHeadModel.DueDate != null ? Convert.ToDateTime(packingHeadModel.DueDate) : (DateTime?)null;
            

                if (packingHeadModel.PacketNo.IsNotNull())
                    jobEntryDTO.BasketID = Convert.ToInt32(packingHeadModel.PacketNo);

                //jobEntryDTO.TransactionHeadID = packingHeadModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(packingHeadModel.ReferenceTransactionNo) : (int?)null;
                jobEntryDTO.TransactionHeadID = packingHeadModel.ReferenceTransaction.IsNotNull() ? Convert.ToInt32(packingHeadModel.ReferenceTransaction) : (int?)null; // how transaction can be null for any job?
                jobEntryDTO.JobStatusID = packingHeadModel.JobStatusID;
                jobEntryDTO.JobSizeID = packingHeadModel.JobSize.IsNotNull() ? Convert.ToInt16(packingHeadModel.JobSize) : (short?)null;
                jobEntryDTO.PriorityID = packingHeadModel.JobPriorityID.IsNotNull() ? Convert.ToByte(packingHeadModel.JobPriorityID) : (byte?)null;
                jobEntryDTO.JobOperationStatusID = Convert.ToByte(packingHeadModel.JobOperationStatus.Key);
                jobEntryDTO.ProcessStartDate = packingHeadModel.ProcessStartDate != null ? Convert.ToDateTime(packingHeadModel.ProcessStartDate) : (DateTime?)null;
                jobEntryDTO.ProcessEndDate = packingHeadModel.ProcessEndDate != null ? Convert.ToDateTime(packingHeadModel.ProcessEndDate) : (DateTime?)null;
                jobEntryDTO.CreatedBy = packingHeadModel.CreatedBy;
                jobEntryDTO.CreatedDate = packingHeadModel.CreatedDate;
                jobEntryDTO.UpdatedBy = packingHeadModel.UpdatedBy;
                jobEntryDTO.UpdatedDate = packingHeadModel.UpdatedDate;
                jobEntryDTO.TimeStamps = packingHeadModel.TimeStamps;

                JobEntryDetailDTO operationDetail = null;

                foreach (var detail in packingDetails)
                {
                    operationDetail = new JobEntryDetailDTO();

                    operationDetail.JobEntryDetailIID = detail.TransactionDetailID;
                    operationDetail.JobEntryHeadID = packingHeadModel.TransactionHeadIID;
                    operationDetail.ProductSKUID = detail.ProductSKUID;
                    operationDetail.UnitPrice = detail.UnitPrice;
                    operationDetail.Quantity = Convert.ToDecimal(detail.Quantity);
                    operationDetail.LocationID = detail.LocationID;
                    //operationDetail.IsQuantiyVerified = detail.IsRemainingQuantity;
                    operationDetail.IsBarCodeVerified = detail.IsVerifyBarCode;
                    operationDetail.IsLocationVerified = detail.IsVerifyLocation;
                    operationDetail.JobStatusID = detail.JobStatusID;
                    operationDetail.Remarks = detail.Remarks;
                    operationDetail.ValidatedQuantity = detail.VerifyQuantity;
                    operationDetail.ValidationBarCode = detail.VerifyBarCode;
                    //operationDetail.ValidatedLocationBarcode = detail.VerifyLocation;
                    operationDetail.CreatedBy = detail.CreatedBy;
                    operationDetail.CreatedDate = detail.CreatedDate.IsNotNull() ? Convert.ToDateTime(detail.CreatedDate) : (DateTime?)null;
                    operationDetail.UpdatedBy = detail.UpdatedBy;
                    operationDetail.UpdatedDate = detail.UpdatedDate.IsNotNull() ? Convert.ToDateTime(detail.UpdatedDate) : (DateTime?)null;
                    operationDetail.TimeStamps = detail.TimeStamps;

                    jobEntryDTO.JobEntryDetails.Add(operationDetail);
                }

                return jobEntryDTO;
            }
            else
            {
                return new JobEntryHeadDTO();
            }
        }


    }

    
}
