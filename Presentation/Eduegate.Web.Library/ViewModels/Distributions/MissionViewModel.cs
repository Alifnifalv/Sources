using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Services.Contracts.Enums;
using Eduegate.Services.Contracts.Warehouses;
using Eduegate.Web.Library.Enums;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    public class MissionViewModel : BaseMasterViewModel
    {

        public MissionViewModel()
        {
            MasterViewModel = new MissionHeadViewModel();
            DetailViewModel = new List<MissionDetailViewModel>();
            Shipment = new ShipmentDetailViewModel() { NoOfPcs = "1", Weight = "0" };
        }

        public MissionHeadViewModel MasterViewModel { get; set; }
        public List<MissionDetailViewModel> DetailViewModel { get; set; }
        public ShipmentDetailViewModel Shipment { get; set; }

        public static MissionViewModel FromJobOperationDTO(List<JobOperationHeadDTO> dtos)
        {
            var dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");

            var VM = new MissionViewModel()
            {
                MasterViewModel = new MissionHeadViewModel() { JobStartDate = DateTime.Today.ToString(dateTimeFormat), JobEndDate = DateTime.Now.ToString(dateTimeFormat),
                                                               JobStatusID = (int)JobStatuses.Assigned,
                                                               JobStatus = (JobStatuses.Assigned).ToString(),
                                                               JobOperationStatusID = (int)JobOperationStatusTypes.NotStarted,
                                                               JobOperationStatus = JobOperationStatusTypes.NotStarted.ToString()
                },
                DetailViewModel = new List<MissionDetailViewModel>()
            };

            foreach (var dto in dtos)
            {
                VM.DetailViewModel.Add(new MissionDetailViewModel()
                {
                    Description = dto.Remarks,
                    JobID = dto.JobEntryHeadIID.ToString(),
                    JobNumber = dto.JobNumber,
                    ShoppingCartID = dto.ShoppingCartID,
                    CityName=dto.CityName,
                    //InvoiceNo = dto.ReferenceTransaction.ToString(),
                });
            }
            return VM;
        }

        public static JobEntryHeadDTO FromVMToJobEntryDTO( MissionViewModel vm)
        {
            var jobEntry = new JobEntryHeadDTO();
            jobEntry.JobEntryDetails = new List<JobEntryDetailDTO>();
            if (vm != null)
            {
                if (vm.MasterViewModel != null)
                {
                    jobEntry.JobEntryHeadIID = vm.MasterViewModel.JobEntryHeadIID;
                    jobEntry.BranchID = vm.MasterViewModel.Branch.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.Branch) : (long?)null;
                    jobEntry.DocumentTypeID = vm.MasterViewModel.DocumentType.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.DocumentType) : (int?)null;
                    jobEntry.JobNumber = vm.MasterViewModel.JobNumber;
                    jobEntry.JobStartDate = Convert.ToDateTime(vm.MasterViewModel.JobStartDate);
                    jobEntry.JobEndDate = Convert.ToDateTime(vm.MasterViewModel.JobEndDate);
                    jobEntry.Remarks = vm.MasterViewModel.Remarks;
                    jobEntry.JobNumber = vm.MasterViewModel.TransactionNo;
                    jobEntry.EmployeeID = vm.MasterViewModel.Driver.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.Driver.Key) : (long?)null;
                    jobEntry.VehicleID = vm.MasterViewModel.Vehicle.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.Vehicle.Key) : (long?)null;
                    jobEntry.ServiceProviderId = vm.MasterViewModel.ServiceProvider.IsNotNull() ? Convert.ToInt32(vm.MasterViewModel.ServiceProvider.Key) : (int?)null;
                    jobEntry.JobStatusID = vm.MasterViewModel.JobStatusID;
                    jobEntry.JobOperationStatusID = vm.MasterViewModel.JobOperationStatusID;
                    jobEntry.PriorityID = vm.MasterViewModel.JobPriority.IsNotNull() ? Convert.ToByte(vm.MasterViewModel.JobPriority) : (byte?)null;
                    jobEntry.CreatedBy = vm.MasterViewModel.CreatedBy;
                    jobEntry.CreatedDate = vm.MasterViewModel.CreatedDate;
                    jobEntry.UpdatedBy = vm.MasterViewModel.UpdatedBy;
                    jobEntry.UpdatedDate = vm.MasterViewModel.UpdatedDate;
                    jobEntry.TimeStamps = vm.MasterViewModel.TimeStamps;
                }

                if (vm.DetailViewModel != null && vm.DetailViewModel.Count > 0)
                {
                    foreach (var detail in vm.DetailViewModel)
                    {
                        var jobDetail = new JobEntryDetailDTO();

                        jobDetail.JobEntryHeadID = detail.JobEntryHeadID;
                        jobDetail.JobEntryDetailIID = detail.JobEntryDetailIID;
                        jobDetail.ParentJobEntryHeadID = detail.JobID.IsNotNull() ? Convert.ToInt32(detail.JobID) : 0;
                        jobDetail.Remarks = detail.Description;
                        jobDetail.CreatedBy = detail.CreatedBy;
                        jobDetail.CreatedDate = detail.CreatedDate;
                        jobDetail.UpdatedBy = detail.UpdatedBy;
                        jobDetail.UpdatedDate = detail.UpdatedDate;
                        jobDetail.TimeStamps = detail.TimeStamps;
                        jobDetail.AWBNo = detail.AWBNo;

                        jobEntry.JobEntryDetails.Add(jobDetail);
                    }
                }
            }

            return jobEntry;
        }

        public static MissionViewModel FromJobEntryDTOToVM(JobEntryHeadDTO dto, List<JobOperationHeadDTO> operationHeadDTO = null)
        {
            var dateTimeFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateTimeFormat");
            var vm = new MissionViewModel()
            {
                MasterViewModel = new MissionHeadViewModel(),
                DetailViewModel = new List<MissionDetailViewModel>()
            };

            if (dto != null)
            {

                vm.MasterViewModel.JobEntryHeadIID = dto.JobEntryHeadIID;
                vm.MasterViewModel.Branch = dto.BranchID.ToString();
                vm.MasterViewModel.DocumentType = dto.DocumentTypeID.ToString();
                vm.MasterViewModel.TransactionNo = dto.JobNumber;
                vm.MasterViewModel.JobStartDate = Convert.ToDateTime(dto.JobStartDate).ToString(dateTimeFormat);
                vm.MasterViewModel.JobEndDate = Convert.ToDateTime(dto.JobEndDate).ToString(dateTimeFormat);
                vm.MasterViewModel.Remarks = dto.Remarks;
                vm.MasterViewModel.ServiceProvider = new KeyValueViewModel();
                vm.MasterViewModel.ServiceProvider.Key = dto.ServiceProviderId.ToString();
                vm.MasterViewModel.ServiceProvider.Value = dto.ServiceProviderName;
                vm.MasterViewModel.Driver = new KeyValueViewModel();
                vm.MasterViewModel.Driver.Key = dto.EmployeeID.ToString();
                vm.MasterViewModel.Driver.Value = dto.EmployeeName;
                vm.MasterViewModel.Vehicle = new KeyValueViewModel();
                vm.MasterViewModel.Vehicle.Key = dto.VehicleID.ToString();
                vm.MasterViewModel.Vehicle.Value = dto.VehicleCode;
                vm.MasterViewModel.JobStatusID = Convert.ToInt32(dto.JobStatusID);
                vm.MasterViewModel.JobStatus = dto.JobStatus;
                vm.MasterViewModel.JobOperationStatusID = dto.JobOperationStatusID;
                vm.MasterViewModel.JobOperationStatus = dto.JobOperationStatus;
                vm.MasterViewModel.JobPriority = dto.PriorityID.ToString();
                vm.MasterViewModel.CreatedBy = dto.CreatedBy;
                vm.MasterViewModel.CreatedDate = dto.CreatedDate;
                vm.MasterViewModel.UpdatedBy = dto.UpdatedBy;
                vm.MasterViewModel.UpdatedDate = dto.UpdatedDate;
                vm.MasterViewModel.TimeStamps = dto.TimeStamps;
                vm.MasterViewModel.IsServiceProver = dto.ServiceProviderId == 2 ? true : false;

                if (dto.JobEntryDetails != null && dto.JobEntryDetails.Count > 0)
                {
                    foreach (var jobDetail in dto.JobEntryDetails)
                    {
                        var detail = new MissionDetailViewModel();

                        if(operationHeadDTO != null)
                        {
                            var operaionHeadDetail = operationHeadDTO.Where(x=> x.JobEntryHeadIID == jobDetail.ParentJobEntryHeadID).FirstOrDefault();
                            detail.JobNumber = operaionHeadDetail.JobNumber;
                            detail.ShoppingCartID = operaionHeadDetail.ShoppingCartID;
                            detail.CityName = operaionHeadDetail.CityName;
                        }

                        detail.JobEntryHeadID = jobDetail.JobEntryHeadID.IsNotNull() ? Convert.ToInt32(jobDetail.JobEntryHeadID) : 0;
                        detail.JobEntryDetailIID = jobDetail.JobEntryDetailIID;
                        detail.JobID = jobDetail.ParentJobEntryHeadID.ToString();
                        detail.Description = jobDetail.Remarks;
                        detail.CreatedBy = jobDetail.CreatedBy;
                        detail.CreatedDate = jobDetail.CreatedDate;
                        detail.UpdatedBy = jobDetail.UpdatedBy;
                        detail.UpdatedDate = jobDetail.UpdatedDate;
                        detail.TimeStamps = jobDetail.TimeStamps;
                        detail.AWBNo = jobDetail.AWBNo;
                        
                        vm.DetailViewModel.Add(detail);
                    }
                }
            }

            return vm;
        }
    }
}
