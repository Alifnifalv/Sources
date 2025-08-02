using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class JobStockOutHeadViewModel : JobOperationHeadViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Button)]
        [DisplayName("")]
        public String GenerateInvoice { get; set; }


        public static JobEntryHeadDTO ToJobEntryDTOFromStockOutHeadAndOperationDetailModel(JobStockOutHeadViewModel stockOutHeadModel, List<JobStockoutDetailViewModel> stockOutDetails)
        {
            JobEntryHeadDTO jobEntryDTO = new JobEntryHeadDTO();
            jobEntryDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

            if (stockOutHeadModel.IsNotNull() && stockOutDetails.Count > 0)
            {
                if (stockOutHeadModel.AssignBackEmployee.IsNotNull())
                    jobEntryDTO.EmployeeID = Convert.ToInt32(stockOutHeadModel.AssignBackEmployee.Key);
                else
                    jobEntryDTO.EmployeeID = stockOutHeadModel.EmployeeID;
                
                if (stockOutHeadModel.IsDoneFlag == true)
                {
                    if (stockOutHeadModel.JobOperationStatus.Key == Convert.ToString((int)JobOperationStatusTypes.InProcess))
                    {                        
                        stockOutHeadModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.Completed);
                        stockOutHeadModel.JobStatusID = (int)JobOperationTypes.Packing;
                        jobEntryDTO.EmployeeID = null;
                    }
                }

                jobEntryDTO.JobEntryHeadIID = stockOutHeadModel.TransactionHeadIID;
                jobEntryDTO.BranchID = stockOutHeadModel.BranchID > 0 ? Convert.ToInt32(stockOutHeadModel.BranchID) : (long?)null;
                jobEntryDTO.DocumentTypeID = stockOutHeadModel.DocumentTypeID > 0 ? Convert.ToInt32(stockOutHeadModel.DocumentTypeID) : (int?)null;
                jobEntryDTO.JobNumber = stockOutHeadModel.JobNumber;
                jobEntryDTO.JobStartDate = stockOutHeadModel.JobDate != null ? Convert.ToDateTime(stockOutHeadModel.JobDate) : (DateTime?)null;
                jobEntryDTO.Remarks = stockOutHeadModel.Remarks;
                jobEntryDTO.JobEndDate = stockOutHeadModel.DueDate != null ? Convert.ToDateTime(stockOutHeadModel.DueDate) : (DateTime?)null;


                //jobEntryDTO.TransactionHeadID = stockOutHeadModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(stockOutHeadModel.ReferenceTransactionNo) : (int?)null;
                jobEntryDTO.TransactionHeadID = stockOutHeadModel.ReferenceTransaction.IsNotNull() ? Convert.ToInt32(stockOutHeadModel.ReferenceTransaction) : (int?)null; // how transaction can be null for any job?
                jobEntryDTO.JobStatusID = stockOutHeadModel.JobStatusID;
                jobEntryDTO.JobSizeID = stockOutHeadModel.JobSize.IsNotNull() ? Convert.ToInt16(stockOutHeadModel.JobSize) : (short?)null;
                jobEntryDTO.PriorityID = stockOutHeadModel.JobPriorityID.IsNotNull() ? Convert.ToByte(stockOutHeadModel.JobPriorityID) : (byte?)null;
                jobEntryDTO.JobOperationStatusID = Convert.ToByte(stockOutHeadModel.JobOperationStatus.Key);
                jobEntryDTO.ProcessStartDate = stockOutHeadModel.ProcessStartDate != null ? Convert.ToDateTime(stockOutHeadModel.ProcessStartDate) : (DateTime?)null;
                jobEntryDTO.ProcessEndDate = stockOutHeadModel.ProcessEndDate != null ? Convert.ToDateTime(stockOutHeadModel.ProcessEndDate) : (DateTime?)null;
                jobEntryDTO.CreatedBy = stockOutHeadModel.CreatedBy;
                jobEntryDTO.CreatedDate = stockOutHeadModel.CreatedDate;
                jobEntryDTO.UpdatedBy = stockOutHeadModel.UpdatedBy;
                jobEntryDTO.UpdatedDate = stockOutHeadModel.UpdatedDate;
                jobEntryDTO.TimeStamps = stockOutHeadModel.TimeStamps;

                JobEntryDetailDTO operationDetail = null;

                foreach (var detail in stockOutDetails)
                {
                    operationDetail = new JobEntryDetailDTO();

                    operationDetail.JobEntryDetailIID = detail.TransactionDetailID;
                    operationDetail.JobEntryHeadID = stockOutHeadModel.TransactionHeadIID;
                    operationDetail.ProductSKUID = detail.ProductSKUID;
                    operationDetail.UnitPrice = detail.UnitPrice;
                    operationDetail.Quantity = Convert.ToDecimal(detail.Quantity);
                    operationDetail.LocationID = detail.LocationID;
                    operationDetail.IsQuantiyVerified = detail.IsVerifyQuantity;
                    operationDetail.IsBarCodeVerified = detail.IsVerifyBarCode;
                    operationDetail.IsLocationVerified = detail.IsVerifyLocation;
                    operationDetail.JobStatusID = detail.JobStatusID;
                    operationDetail.Remarks = detail.Remarks;
                    operationDetail.ValidatedQuantity = detail.VerifyQuantity;
                    //operationDetail.ValidatedPartNo = detail.VerifyPartNo;
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
