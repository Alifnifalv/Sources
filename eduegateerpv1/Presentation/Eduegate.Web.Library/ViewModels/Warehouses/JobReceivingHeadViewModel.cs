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
    public class JobReceivingHeadViewModel : JobOperationHeadViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Button)]
        [DisplayName("Generate Invoice")]
        public string GenerateInvoice { get; set; }

        public static JobEntryHeadDTO ToJobEntryDTOFromReceivingHeadAndDetailModel(JobReceivingHeadViewModel receivingHeadModel, List<JobRecevingDetailViewModel> receivingDetails)
        {
            JobEntryHeadDTO jobEntryDTO = new JobEntryHeadDTO();
            jobEntryDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

            if (receivingHeadModel.IsNotNull() && receivingDetails.Count > 0)
            {
                if(receivingHeadModel.AssignBackEmployee.IsNotNull())
                    jobEntryDTO.EmployeeID = Convert.ToInt32(receivingHeadModel.AssignBackEmployee.Key);
                else
                    jobEntryDTO.EmployeeID = receivingHeadModel.EmployeeID;

                if (receivingHeadModel.IsDoneFlag == true)
                {
                    if(receivingHeadModel.JobOperationStatus.Key == Convert.ToString((int)JobOperationStatusTypes.InProcess)){
                        receivingHeadModel.JobOperationStatus.Key = Convert.ToString((int)JobOperationStatusTypes.Completed);
                        receivingHeadModel.JobStatusID = (int)JobOperationTypes.PutAway;
                        jobEntryDTO.EmployeeID = null;
                    }
                }

                jobEntryDTO.JobEntryHeadIID = receivingHeadModel.TransactionHeadIID;
                jobEntryDTO.BranchID = Convert.ToInt32(receivingHeadModel.BranchID);
                jobEntryDTO.DocumentTypeID = Convert.ToInt32(receivingHeadModel.DocumentTypeID);
                jobEntryDTO.JobNumber = receivingHeadModel.JobNumber;
                jobEntryDTO.JobStartDate = receivingHeadModel.JobDate != null ? Convert.ToDateTime(receivingHeadModel.JobDate) : (DateTime?)null;
                jobEntryDTO.Remarks = receivingHeadModel.Remarks;
                jobEntryDTO.JobEndDate = receivingHeadModel.DueDate != null ? Convert.ToDateTime(receivingHeadModel.DueDate) : (DateTime?)null;


                //jobEntryDTO.TransactionHeadID = receivingHeadModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(receivingHeadModel.ReferenceTransactionNo) : (int?)null;
                jobEntryDTO.TransactionHeadID = receivingHeadModel.ReferenceTransaction.IsNotNull() ? Convert.ToInt32(receivingHeadModel.ReferenceTransaction) : (int?)null;
                jobEntryDTO.JobStatusID = receivingHeadModel.JobStatusID;
                jobEntryDTO.JobSizeID = Convert.ToInt16(receivingHeadModel.JobSize);
                jobEntryDTO.PriorityID = Convert.ToByte(receivingHeadModel.JobPriorityID);
                jobEntryDTO.JobOperationStatusID = Convert.ToByte(receivingHeadModel.JobOperationStatus.Key);
                jobEntryDTO.ProcessStartDate = receivingHeadModel.ProcessStartDate != null ? Convert.ToDateTime(receivingHeadModel.ProcessStartDate) : (DateTime?)null;
                jobEntryDTO.ProcessEndDate = receivingHeadModel.ProcessEndDate != null ? Convert.ToDateTime(receivingHeadModel.ProcessEndDate) : (DateTime?)null;
                jobEntryDTO.CreatedBy = receivingHeadModel.CreatedBy;
                jobEntryDTO.CreatedDate = receivingHeadModel.CreatedDate;
                jobEntryDTO.UpdatedBy = receivingHeadModel.UpdatedBy;
                jobEntryDTO.UpdatedDate = receivingHeadModel.UpdatedDate;
                jobEntryDTO.TimeStamps = receivingHeadModel.TimeStamps;

                JobEntryDetailDTO operationDetail = null;

                foreach (var detail in receivingDetails)
                {
                    operationDetail = new JobEntryDetailDTO();

                    operationDetail.JobEntryDetailIID = detail.TransactionDetailID;
                    operationDetail.JobEntryHeadID = receivingHeadModel.TransactionHeadIID;
                    operationDetail.ProductSKUID = detail.ProductSKUID;
                    operationDetail.UnitPrice = Convert.ToDecimal(detail.VerifyPrice);
                    operationDetail.Quantity = Convert.ToDecimal(detail.Quantity);
                    operationDetail.LocationID = Convert.ToInt32(detail.LocationID);
                    operationDetail.IsQuantiyVerified = detail.IsVerifyQuantity;
                    operationDetail.IsBarCodeVerified = detail.IsVerifyBarCode;
                    operationDetail.IsLocationVerified = detail.IsVerifyLocation;
                    operationDetail.JobStatusID = detail.JobStatusID;
                    operationDetail.Remarks = detail.Remarks;
                    operationDetail.ValidatedQuantity = detail.VerifyQuantity;
                    operationDetail.ValidatedPartNo = detail.VerifyPartNo;
                    operationDetail.ValidationBarCode = detail.VerifyBarCode;
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
