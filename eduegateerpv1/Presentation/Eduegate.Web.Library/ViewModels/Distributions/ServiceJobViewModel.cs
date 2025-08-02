using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Warehouses;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class ServiceJobViewModel : BaseMasterViewModel
    {
        public ServiceJobViewModel()
        {
            MasterViewModel = new ServiceJobHeadViewModel();
            DetailViewModel = new List<ServiceJobDetailViewModel>() { new ServiceJobDetailViewModel() };
        }

        public ServiceJobHeadViewModel MasterViewModel { get; set; }
        public List<ServiceJobDetailViewModel> DetailViewModel { get; set; }


        public static ServiceJobViewModel FromJobEntryDTOToVM(JobEntryHeadDTO dto)
        {
            var dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
            var vm = new ServiceJobViewModel()
            {
                MasterViewModel = new ServiceJobHeadViewModel(),
                DetailViewModel = new List<ServiceJobDetailViewModel>()
            };

            if (dto != null)
            {
                vm.MasterViewModel.JobEntryHeadIID = dto.JobEntryHeadIID;
                vm.MasterViewModel.Branch = new KeyValueViewModel() { Key = dto.BranchID.ToString(), Value = dto.BranchName };
                vm.MasterViewModel.DocumentType = new KeyValueViewModel() { Key = dto.DocumentTypeID.ToString(), Value = dto.DocumentTypeName };
                vm.MasterViewModel.TransactionNo = dto.JobNumber;
                vm.MasterViewModel.JobStartDate = Convert.ToDateTime(dto.JobStartDate).ToString(dateTimeFormat);
                vm.MasterViewModel.JobEndDate = Convert.ToDateTime(dto.JobEndDate).ToString(dateTimeFormat);
                vm.MasterViewModel.Remarks = dto.Remarks;
                vm.MasterViewModel.JobStatus = dto.JobStatus;
                vm.MasterViewModel.JobOperationStatus = dto.JobOperationStatus;
                vm.MasterViewModel.JobPriority = dto.PriorityID.ToString();
                vm.MasterViewModel.CreatedBy = dto.CreatedBy;
                vm.MasterViewModel.CreatedDate = dto.CreatedDate;
                vm.MasterViewModel.UpdatedBy = dto.UpdatedBy;
                vm.MasterViewModel.UpdatedDate = dto.UpdatedDate;
                vm.MasterViewModel.TimeStamps = dto.TimeStamps;
                vm.MasterViewModel.JobActivity = dto.JobActivityID.HasValue ? dto.JobActivityID.Value.ToString() : null;
                vm.MasterViewModel.JobStatusId = dto.JobStatusID;
                vm.MasterViewModel.ReferenceTransactionNo = dto.TransactionHeadID.ToString();
                vm.MasterViewModel.ReferenceTransaction = dto.TransactionNumber;

                if (dto.JobEntryDetails != null && dto.JobEntryDetails.Count > 0)
                {
                    foreach (var jobDetail in dto.JobEntryDetails)
                    {
                        var detail = new ServiceJobDetailViewModel();

                        detail.JobEntryHeadID = jobDetail.JobEntryHeadID.IsNotNull() ? Convert.ToInt32(jobDetail.JobEntryHeadID) : 0;
                        detail.JobEntryDetailIID = jobDetail.JobEntryDetailIID;
                        detail.SKUID = new KeyValueViewModel() { Key = jobDetail.ProductSKUID.ToString(), Value = jobDetail.ProductSkuName };
                        detail.Quantity = jobDetail.Quantity;
                        detail.Description = jobDetail.Remarks;
                        detail.CreatedBy = jobDetail.CreatedBy;
                        detail.CreatedDate = jobDetail.CreatedDate;
                        detail.UpdatedBy = jobDetail.UpdatedBy;
                        detail.UpdatedDate = jobDetail.UpdatedDate;
                        detail.TimeStamps = jobDetail.TimeStamps;
                        vm.DetailViewModel.Add(detail);
                    }
                }
            }

            return vm;
        }

        public static JobEntryHeadDTO FromVMToJobEntryDTO(ServiceJobViewModel vm)
        {
            var jobEntry = new JobEntryHeadDTO();
            jobEntry.JobEntryDetails = new List<JobEntryDetailDTO>();
            if (vm != null)
            {
                if (vm.MasterViewModel != null)
                {
                    jobEntry.JobEntryHeadIID = vm.MasterViewModel.JobEntryHeadIID;
                    jobEntry.BranchID = vm.MasterViewModel.Branch.IsNotNull() && !string.IsNullOrEmpty(vm.MasterViewModel.Branch.Key) ? Convert.ToInt32(vm.MasterViewModel.Branch.Key) : (long?)null;
                    jobEntry.DocumentTypeID = vm.MasterViewModel.DocumentType.IsNotNull() && !string.IsNullOrEmpty(vm.MasterViewModel.DocumentType.Key) ? Convert.ToInt32(vm.MasterViewModel.DocumentType.Key) : (int?)null;
                    jobEntry.JobNumber = vm.MasterViewModel.JobNumber;
                    jobEntry.JobStartDate = Convert.ToDateTime(vm.MasterViewModel.JobStartDate);
                    jobEntry.JobEndDate = Convert.ToDateTime(vm.MasterViewModel.JobEndDate);
                    jobEntry.Remarks = vm.MasterViewModel.Remarks;
                    jobEntry.JobNumber = vm.MasterViewModel.TransactionNo;
                    //jobEntry.JobStatusID = vm.MasterViewModel.JobStatusID;
                    //jobEntry.JobOperationStatusID = vm.MasterViewModel.JobOperationStatusID;
                    jobEntry.PriorityID = vm.MasterViewModel.JobPriority.IsNotNull() ? Convert.ToByte(vm.MasterViewModel.JobPriority) : (byte?)null;
                    jobEntry.CreatedBy = vm.MasterViewModel.CreatedBy;
                    jobEntry.CreatedDate = vm.MasterViewModel.CreatedDate;
                    jobEntry.UpdatedBy = vm.MasterViewModel.UpdatedBy;
                    jobEntry.UpdatedDate = vm.MasterViewModel.UpdatedDate;
                    jobEntry.TimeStamps = vm.MasterViewModel.TimeStamps;
                    jobEntry.JobActivityID = int.Parse(vm.MasterViewModel.JobActivity);
                    jobEntry.TransactionHeadID = vm.MasterViewModel.ReferenceTransactionNo.IsNotNull() ? long.Parse(vm.MasterViewModel.ReferenceTransactionNo) : (long?)null;
                }

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var detail in vm.DetailViewModel)
                    {
                        if (detail.SKUID != null && !string.IsNullOrEmpty(detail.SKUID.Key))
                        {
                            var jobDetail = new JobEntryDetailDTO();

                            jobDetail.JobEntryHeadID = detail.JobEntryHeadID;
                            jobDetail.JobEntryDetailIID = detail.JobEntryDetailIID;
                            //jobDetail.ParentJobEntryHeadID = detail.JobID.IsNotNull() ? Convert.ToInt32(detail.JobID) : 0;
                            jobDetail.Remarks = detail.Description;
                            jobDetail.CreatedBy = detail.CreatedBy;
                            jobDetail.CreatedDate = detail.CreatedDate;
                            jobDetail.UpdatedBy = detail.UpdatedBy;
                            jobDetail.UpdatedDate = detail.UpdatedDate;
                            jobDetail.TimeStamps = detail.TimeStamps;
                            jobDetail.ProductSKUID = long.Parse(detail.SKUID.Key);
                            jobDetail.Quantity = detail.Quantity;
                            jobEntry.JobEntryDetails.Add(jobDetail);
                        }
                    }
                }
            }

            return jobEntry;
        }
    }
}
