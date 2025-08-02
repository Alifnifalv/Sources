using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Web.Library.Enums;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    [Order(1)]
    public class JobPickingHeadViewModel : JobOperationHeadViewModel
    {
        [Order(15)]
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Basket")]
        [Select2("Basket", "Numeric", false)]
        [LookUp("LookUps.Basket")]
        public KeyValueViewModel Basket { get; set; }


        public static JobEntryHeadDTO ToJobEntryDTOFromJobPickingHeadAndDetailModel(JobPickingHeadViewModel pickingHeadModel, List<JobPickingDetailViewModel> pickingDetails)
        {
            JobEntryHeadDTO jobEntryDTO = new JobEntryHeadDTO();
            jobEntryDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

            if (pickingHeadModel.IsNotNull() && pickingDetails.Count > 0)
            {
                if (pickingHeadModel.AssignBackEmployee.IsNotNull())
                    jobEntryDTO.EmployeeID = Convert.ToInt32(pickingHeadModel.AssignBackEmployee.Key);
                else
                    jobEntryDTO.EmployeeID = pickingHeadModel.EmployeeID;
                if (pickingHeadModel.IsDoneFlag == true)
                {
                    if (pickingHeadModel.JobOperationStatus.Key == Convert.ToString((int)JobOperationStatusTypes.InProcess))
                    {
                        pickingHeadModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.Completed);
                        pickingHeadModel.JobStatusID = (int)JobOperationTypes.StockOut;
                        jobEntryDTO.EmployeeID = null;
                    }
                }

                jobEntryDTO.JobEntryHeadIID = pickingHeadModel.TransactionHeadIID;
                jobEntryDTO.BranchID = pickingHeadModel.BranchID > 0 ? Convert.ToInt32(pickingHeadModel.BranchID) : (long?)null;
                jobEntryDTO.DocumentTypeID = pickingHeadModel.DocumentTypeID > 0 ? Convert.ToInt32(pickingHeadModel.DocumentTypeID) : (int?)null;
                jobEntryDTO.JobNumber = pickingHeadModel.JobNumber;
                jobEntryDTO.JobStartDate = pickingHeadModel.JobDate != null ? Convert.ToDateTime(pickingHeadModel.JobDate) : (DateTime?)null;
                jobEntryDTO.Remarks = pickingHeadModel.Remarks;
                jobEntryDTO.JobEndDate = pickingHeadModel.DueDate != null ? Convert.ToDateTime(pickingHeadModel.DueDate) : (DateTime?)null;
               

                if (pickingHeadModel.Basket.IsNotNull())
                    jobEntryDTO.BasketID = Convert.ToInt32(pickingHeadModel.Basket.Key);

                //jobEntryDTO.TransactionHeadID = pickingHeadModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(pickingHeadModel.ReferenceTransactionNo) : (int?)null;
                jobEntryDTO.TransactionHeadID = pickingHeadModel.ReferenceTransaction.IsNotNull() ? Convert.ToInt32(pickingHeadModel.ReferenceTransaction) : (int?)null; // how transaction can be null for any job?
                jobEntryDTO.JobStatusID = pickingHeadModel.JobStatusID;
                jobEntryDTO.JobSizeID = pickingHeadModel.JobSize.IsNotNull() ? Convert.ToInt16(pickingHeadModel.JobSize) : (short?)null;
                jobEntryDTO.PriorityID = pickingHeadModel.JobPriorityID.IsNotNull() ? Convert.ToByte(pickingHeadModel.JobPriorityID) : (byte?)null;
                jobEntryDTO.JobOperationStatusID = Convert.ToByte(pickingHeadModel.JobOperationStatus.Key);
                jobEntryDTO.ProcessStartDate = pickingHeadModel.ProcessStartDate != null ? Convert.ToDateTime(pickingHeadModel.ProcessStartDate) : (DateTime?)null;
                jobEntryDTO.ProcessEndDate = pickingHeadModel.ProcessEndDate != null ? Convert.ToDateTime(pickingHeadModel.ProcessEndDate) : (DateTime?)null;
                jobEntryDTO.CreatedBy = pickingHeadModel.CreatedBy;
                jobEntryDTO.CreatedDate = pickingHeadModel.CreatedDate;
                jobEntryDTO.UpdatedBy = pickingHeadModel.UpdatedBy;
                jobEntryDTO.UpdatedDate = pickingHeadModel.UpdatedDate;
                jobEntryDTO.TimeStamps = pickingHeadModel.TimeStamps;

                JobEntryDetailDTO operationDetail = null;

                foreach (var detail in pickingDetails)
                {
                    operationDetail = new JobEntryDetailDTO();

                    operationDetail.JobEntryDetailIID = detail.TransactionDetailID;
                    operationDetail.JobEntryHeadID = pickingHeadModel.TransactionHeadIID;
                    operationDetail.ProductSKUID = detail.ProductSKUID;
                    operationDetail.UnitPrice = detail.UnitPrice;
                    operationDetail.Quantity = Convert.ToDecimal(detail.Quantity);
                    operationDetail.LocationID = detail.LocationID;
                    operationDetail.IsQuantiyVerified = detail.IsRemainingQuantity;
                    operationDetail.IsBarCodeVerified = detail.IsVerifyBarCode;
                    operationDetail.IsLocationVerified = detail.IsVerifyLocation;
                    operationDetail.JobStatusID = detail.JobStatusID;
                    operationDetail.Remarks = detail.Remarks;
                    operationDetail.ValidatedQuantity = detail.VerifyQuantity;
                    operationDetail.ValidationBarCode = detail.VerifyBarCode;
                    operationDetail.ValidatedLocationBarcode = detail.VerifyLocation;
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
