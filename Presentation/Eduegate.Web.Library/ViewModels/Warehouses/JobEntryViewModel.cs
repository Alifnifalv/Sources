using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Domain;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    public class JobEntryViewModel : BaseMasterViewModel
    {
        public JobEntryViewModel()
        {
            MasterViewModel = new JobEntryMasterViewModel();
            DetailViewModel = new List<JobEntryDetailViewModel>() { new JobEntryDetailViewModel() };
        }

        public JobEntryMasterViewModel MasterViewModel { get; set; }
        public List<JobEntryDetailViewModel> DetailViewModel { get; set; }

        public static JobEntryHeadDTO ToDTOFromJobEntryVM(JobEntryViewModel vm) //Translating job entry data from viewmodel to DTO
        {
            if (vm.IsNotNull())
            {
                JobEntryHeadDTO jobEntryDTO = new JobEntryHeadDTO();
                jobEntryDTO.JobEntryDetails = new List<JobEntryDetailDTO>();

                if (vm.MasterViewModel.IsNotNull())
                {
                    jobEntryDTO.JobEntryHeadIID = vm.MasterViewModel.JobEntryHeadIID;
                    jobEntryDTO.BranchID = vm.MasterViewModel.Branch == null || vm.MasterViewModel.Branch.Key == null ? (long?) null : Convert.ToInt32(vm.MasterViewModel.Branch.Key);
                    jobEntryDTO.DocumentTypeID = vm.MasterViewModel.DocumentType.Key == null ? (int?)null: Convert.ToInt32(vm.MasterViewModel.DocumentType.Key);
                    jobEntryDTO.JobNumber = vm.MasterViewModel.TransactionNo;
                    jobEntryDTO.JobStartDate = vm.MasterViewModel.JobStartDate != null ? Convert.ToDateTime(vm.MasterViewModel.JobStartDate) : (DateTime?)null;
                    jobEntryDTO.Remarks = vm.MasterViewModel.Remarks;
                    jobEntryDTO.JobEndDate = vm.MasterViewModel.JobStartDate != null ? Convert.ToDateTime(vm.MasterViewModel.JobEndDate) : (DateTime?)null;
                    jobEntryDTO.EmployeeID = vm.MasterViewModel.Employee.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.Employee.Key) : (long?)null;
                    jobEntryDTO.TransactionHeadID = vm.MasterViewModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionNo) : (int?)null;
                    jobEntryDTO.JobStatusID = Convert.ToInt32(vm.MasterViewModel.JobStatus);
                    jobEntryDTO.PriorityID = Convert.ToByte(vm.MasterViewModel.JobPriority);
                    jobEntryDTO.JobOperationStatusID = !string.IsNullOrEmpty(vm.MasterViewModel.JobOperationStatus) ? Convert.ToByte(vm.MasterViewModel.JobOperationStatus) : (byte?)null;
                    jobEntryDTO.BasketID = !string.IsNullOrEmpty(vm.MasterViewModel.Basket) ? Convert.ToInt32(vm.MasterViewModel.Basket) : (int?)null;
                    jobEntryDTO.ProcessStartDate = vm.MasterViewModel.ProcessStartDate != null ? Convert.ToDateTime(vm.MasterViewModel.ProcessStartDate) : (DateTime?)null;
                    jobEntryDTO.ProcessEndDate = vm.MasterViewModel.ProcessEndDate != null ? Convert.ToDateTime(vm.MasterViewModel.ProcessEndDate) : (DateTime?)null;
                    jobEntryDTO.CreatedBy = vm.MasterViewModel.CreatedBy;
                    jobEntryDTO.CreatedDate = vm.MasterViewModel.CreatedDate;
                    jobEntryDTO.UpdatedBy = vm.MasterViewModel.UpdatedBy;
                    jobEntryDTO.UpdatedDate = vm.MasterViewModel.UpdatedDate;
                    jobEntryDTO.TimeStamps = vm.MasterViewModel.TimeStamps;
                    //jobEntryDTO.ReferenceDocumentTypeID = vm.MasterViewModel.ReferenceTransactionNo.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.ReferenceTransactionNo) : (int?)null;
                    //jobEntryDTO.TransactionNo = vm.MasterViewModel.ReferenceTransactionNo;
                    if (vm.MasterViewModel.DeliveryDetails != null)
                    {
                        jobEntryDTO.OrderContactMap = Inventory.DeliveryAddressViewModel.ToDTO(vm.MasterViewModel.DeliveryDetails);
                    }
                }
                if (vm.DetailViewModel.IsNotNull() && vm.DetailViewModel.Count > 0)
                {
                    foreach (var detail in vm.DetailViewModel)
                    {
                        var jobEntryDetailDTO = new JobEntryDetailDTO();

                        jobEntryDetailDTO.JobEntryDetailIID = detail.JobEntryDetailIID;
                        jobEntryDetailDTO.JobEntryHeadID = vm.MasterViewModel.JobEntryHeadIID; 

                        if(detail.SKUID.IsNotNull())
                            jobEntryDetailDTO.ProductSKUID = Convert.ToInt32(detail.SKUID.Key);

                        jobEntryDetailDTO.Quantity = Convert.ToDecimal(detail.Quantity);
                        jobEntryDetailDTO.LocationID = Convert.ToInt32(detail.LocationID);
                        jobEntryDetailDTO.IsQuantiyVerified = detail.IsQuantiyVerified;
                        jobEntryDetailDTO.IsBarCodeVerified = detail.IsBarCodeVerified;
                        jobEntryDetailDTO.IsLocationVerified = detail.IsLocationVerified;
                        jobEntryDetailDTO.JobStatusID = detail.JobStatusID;
                        jobEntryDetailDTO.ValidatedQuantity = detail.ValidatedQuantity;
                        jobEntryDetailDTO.ValidatedLocationBarcode = detail.ValidatedLocationBarcode;
                        jobEntryDetailDTO.ValidatedPartNo = detail.ValidatedPartNo;
                        jobEntryDetailDTO.ValidationBarCode = detail.ValidationBarCode;
                        jobEntryDetailDTO.Remarks = detail.Remarks;
                        jobEntryDetailDTO.CreatedBy = detail.CreatedBy;
                        jobEntryDetailDTO.CreatedDate = detail.CreatedDate.IsNotNull() ? Convert.ToDateTime(detail.CreatedDate):(DateTime?)null;
                        jobEntryDetailDTO.UpdatedBy = detail.UpdatedBy;
                        jobEntryDetailDTO.UpdatedDate = detail.UpdatedDate.IsNotNull() ? Convert.ToDateTime(detail.UpdatedDate) : (DateTime?)null;
                        jobEntryDetailDTO.TimeStamps = detail.TimeStamps;

                        jobEntryDTO.JobEntryDetails.Add(jobEntryDetailDTO);
                    }
                }

                return jobEntryDTO;
            }
            else
            {
                return new JobEntryHeadDTO();
            }
        }

        public static JobEntryViewModel FromDTOToJobEntryVM(JobEntryHeadDTO dto) //Translating job entry data from DTO to viewmodel
        {
            if (dto.IsNotNull())
            {
                string imageRootUrl = new Domain.Setting.SettingBL().GetSettingValue<string>("ImageHostUrl");
                var vm = new JobEntryViewModel();

                vm.MasterViewModel.JobEntryHeadIID = dto.JobEntryHeadIID;
                vm.MasterViewModel.Branch = dto.BranchID.HasValue ? new KeyValueViewModel() { Key = dto.BranchID.ToString(), Value = dto.BranchName } : null;
                vm.MasterViewModel.DocumentType = dto.DocumentTypeID.HasValue ? new KeyValueViewModel() { Key = dto.DocumentTypeID.ToString(), Value = dto.DocumentTypeName } : null ;
                vm.MasterViewModel.TransactionNo = dto.JobNumber;
                vm.MasterViewModel.JobNumber = dto.JobNumber;
                vm.MasterViewModel.JobStartDate = dto.JobStartDate.ToString();
                vm.MasterViewModel.Remarks = dto.Remarks;
                vm.MasterViewModel.JobEndDate = dto.JobEndDate.ToString();
                vm.MasterViewModel.Employee = new KeyValueViewModel();
                vm.MasterViewModel.Employee.Key = dto.EmployeeID.ToString();
                vm.MasterViewModel.Employee.Value = dto.EmployeeName;
                vm.MasterViewModel.ReferenceTransactionNo = dto.TransactionHeadID.ToString();
                vm.MasterViewModel.JobStatus = dto.JobStatusID.ToString();
                vm.MasterViewModel.JobOperationStatus = dto.JobOperationStatusID.ToString();
                vm.MasterViewModel.JobPriority = dto.PriorityID.ToString();
                vm.MasterViewModel.Basket = dto.BasketID.ToString();
                vm.MasterViewModel.ProcessStartDate = dto.ProcessStartDate.ToString();
                vm.MasterViewModel.ProcessEndDate = dto.ProcessEndDate.ToString();
                //vm.MasterViewModel.ReferenceTransactionNo = dto.ReferenceDocumentTypeID.ToString();
                vm.MasterViewModel.CreatedBy = dto.CreatedBy;
                //vm.MasterViewModel.TransactionNo = dto.TransactionNo;
                vm.MasterViewModel.CreatedDate = dto.CreatedDate;
                vm.MasterViewModel.UpdatedBy = dto.UpdatedBy;
                vm.MasterViewModel.UpdatedDate = dto.UpdatedDate;
                vm.MasterViewModel.TimeStamps = dto.TimeStamps;

                if (dto.OrderContactMap != null)
                {
                    vm.MasterViewModel.DeliveryDetails = Inventory.DeliveryAddressViewModel.FromOrderContactDTOToVM(dto.OrderContactMap);
                }

                if (dto.JobEntryDetails.IsNotNull() && dto.JobEntryDetails.Count > 0)
                {
                    vm.DetailViewModel = new List<JobEntryDetailViewModel>();

                    foreach (JobEntryDetailDTO detailDTO in dto.JobEntryDetails)
                    {
                        var jobEntryDetailViewModel = new JobEntryDetailViewModel();

                        jobEntryDetailViewModel.JobEntryDetailIID = detailDTO.JobEntryDetailIID;
                        jobEntryDetailViewModel.JobEntryHeadID = detailDTO.JobEntryHeadID;

                        if (jobEntryDetailViewModel.SKUID.IsNull())
                            jobEntryDetailViewModel.SKUID = new KeyValueViewModel();

                        jobEntryDetailViewModel.SKUID.Key = detailDTO.ProductSKUID.ToString();
                        jobEntryDetailViewModel.SKUID.Value = detailDTO.ProductSkuName;
                        jobEntryDetailViewModel.Description = detailDTO.ProductSkuName;
                        jobEntryDetailViewModel.Quantity = Convert.ToInt32(detailDTO.Quantity);
                        jobEntryDetailViewModel.LocationID = detailDTO.LocationID;
                        jobEntryDetailViewModel.LocationName = detailDTO.LocationName;
                        jobEntryDetailViewModel.LocationBarcode = detailDTO.LocationBarcode;
                        jobEntryDetailViewModel.BarCode = detailDTO.BarCode;
                        jobEntryDetailViewModel.ProductImage = detailDTO.ProductImage.IsNotNull() ? string.Concat(imageRootUrl,"Products/", detailDTO.ProductIID, "/", detailDTO.ProductImage) : string.Empty; //From DB we will be getting like this 103481\SmallImage\1.jpg
                        jobEntryDetailViewModel.IsQuantiyVerified = detailDTO.IsQuantiyVerified;
                        jobEntryDetailViewModel.IsBarCodeVerified = detailDTO.IsBarCodeVerified;
                        jobEntryDetailViewModel.IsLocationVerified = detailDTO.IsLocationVerified;
                        jobEntryDetailViewModel.JobStatusID = detailDTO.JobStatusID;
                        jobEntryDetailViewModel.ValidatedQuantity = detailDTO.ValidatedQuantity;
                        jobEntryDetailViewModel.ValidatedLocationBarcode = detailDTO.ValidatedLocationBarcode;
                        jobEntryDetailViewModel.ValidatedPartNo = detailDTO.ValidatedPartNo;
                        jobEntryDetailViewModel.ValidationBarCode = detailDTO.ValidationBarCode;
                        jobEntryDetailViewModel.Remarks = detailDTO.Remarks;
                        jobEntryDetailViewModel.CreatedBy = detailDTO.CreatedBy;
                        jobEntryDetailViewModel.CreatedDate = detailDTO.CreatedDate;
                        jobEntryDetailViewModel.UpdatedBy = detailDTO.UpdatedBy;
                        jobEntryDetailViewModel.UpdatedDate = detailDTO.UpdatedDate;
                        jobEntryDetailViewModel.TimeStamps = detailDTO.TimeStamps;
                        //jobEntryDetailViewModel.TransactionNo = detailDTO.TransactionNo;

                        vm.DetailViewModel.Add(jobEntryDetailViewModel);
                    }
                }

                return vm;
            }
            else
            {
                return new JobEntryViewModel();
            } 
        }
    }
}