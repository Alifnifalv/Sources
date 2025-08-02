using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Inventory;
using Eduegate.Services.Contracts.Jobs;
using Eduegate.Services.Contracts.School.Forms;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Eduegate.Web.Library.ViewModels.Inventory;
using Hangfire.Common;
using Newtonsoft.Json;

namespace Eduegate.Web.Library.HR.Career
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "JobManagement", "CRUDModel.ViewModel")]
    [DisplayName("Job Application Management")] 
    public class JobApplicationManagementViewModel : BaseMasterViewModel
    {
        public JobApplicationManagementViewModel()
        {
            HideShortListedData = false;
            FilterBy = new FilterApplicantByViewModel();
            ShortList = new List<JobApplicationShortListViewModel>() { new JobApplicationShortListViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.DataPicker)]
        [CustomDisplay("Job applications for")]
        [DataPicker("CareerListing")]
        public string JobTitle { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [CustomDisplay("")]
        public string NewLine3 { get; set; }
        public long JobIID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.FieldSet, "fullwidth", isFullColumn: false)]
        [CustomDisplay("Filter Applicant by")]
        public FilterApplicantByViewModel FilterBy { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, attribs: "ng-change='HideShortListCheckBox($event, $element, CRUDModel.ViewModel)'")]
        [CustomDisplay("Hide shortlisted applicants")]
        public bool HideShortListedData { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Short list")]
        public List<JobApplicationShortListViewModel> ShortList { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.FieldSet, "ApplicantFilter", "CRUDModel.ViewModel.ApplicantFilter")]
        public class FilterApplicantByViewModel : BaseMasterViewModel
        {

            [ControlType(Framework.Enums.ControlTypes.TextBox)]
            [MaxLength(100, ErrorMessage = "Maximum Length should be within 100!")]
            [CustomDisplay("Name")]
            [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
            public string Name { get; set; }

            [ControlType(Framework.Enums.ControlTypes.TextBox)]
            [MaxLength(2, ErrorMessage = "Maximum Length should be within 2!")]
            [CustomDisplay("Total year of experience")]
            public int? TotalYearOfExperience { get; set; }

            [ControlType(Framework.Enums.ControlTypes.TextBox)]
            [MaxLength(200, ErrorMessage = "Maximum Length should be within 200!")]
            [CustomDisplay("Education")]
            [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Use alphabets only")]
            public string Education { get; set; }

            [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='ClearShortList($event)'")]
            [CustomDisplay("Clear")]
            public string ClearFilter { get; set; }

            [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='FilterShortList($event,CRUDModel.ViewModel.ApplicantFilter)'")]
            [CustomDisplay("Filter")]
            public string Filiter1 { get; set; } 
            
            [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='ReloadShortList($event,CRUDModel.ViewModel.ApplicantFilter)'")]
            [CustomDisplay("Reload")]
            public string Reload { get; set; }
        }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as JobsDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobApplicationManagementViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<JobsDTO, JobApplicationManagementViewModel>.CreateMap();
            var jobdto = dto as JobsDTO;
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var vm = Mapper<JobsDTO, JobApplicationManagementViewModel>.Map(dto as JobsDTO);

            vm.JobIID = jobdto.JobIID;
            vm.JobTitle = jobdto.JobTitle;

            vm.ShortList = new List<JobApplicationShortListViewModel>();

            foreach(var data in jobdto.JobApplicationDTO.Where(x => x.ApplicationIID != 0))
            {
                vm.ShortList.Add(new JobApplicationShortListViewModel()
                {
                    ApplicationIID = data.ApplicationIID,
                    ApplicantID = data.ApplicantID,
                    ApplicantName = data.ApplicantName,
                    AppliedDateString = data.AppliedDate.HasValue ? data.AppliedDate.Value.ToString(dateFormat,CultureInfo.InvariantCulture) : null,
                    Education = data.Education,
                    TotalYearOfExperience = data.TotalYearOfExperience,
                    CVContentID = data.CVContentID,
                    Remarks = data.Remarks,
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<JobApplicationManagementViewModel, JobsDTO>.CreateMap();
            var dto = Mapper<JobApplicationManagementViewModel, JobsDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.JobTitle = this.JobTitle;
            dto.JobIID = this.JobIID;

            dto.JobApplicationDTO = new List<JobApplicationDTO>();

            foreach(var vmList in this.ShortList.Where(x => x.ApplicationIID != 0))
            {
                dto.JobApplicationDTO.Add(new JobApplicationDTO()
                {
                    ApplicationIID = vmList.ApplicationIID,
                    JobID = vmList.JobID,
                    ApplicantID = vmList.ApplicantID,
                    AppliedDate = string.IsNullOrEmpty(vmList.AppliedDateString) ? (DateTime?)null : DateTime.ParseExact(vmList.AppliedDateString, dateFormat, CultureInfo.InvariantCulture),
                    ApplicantName = vmList.ApplicantName,
                    Education = vmList.Education,
                    TotalYearOfExperience = vmList.TotalYearOfExperience,
                    CVContentID = vmList.CVContentID,
                });
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobsDTO>(jsonString);
        }

        //to fill details via advance search
        public static JobApplicationManagementViewModel FromDTOtoVM(List<JobApplicationDTO> dto)
        {
            var vm = new JobApplicationManagementViewModel();
            vm.ShortList = new List<JobApplicationShortListViewModel>();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            if (dto.Count > 0)
            {
                vm.JobTitle = dto.FirstOrDefault().JobTitle;
                vm.JobIID = (long)dto.FirstOrDefault().JobID;

                if (dto != null)
                {
                    foreach (var detailDat in dto)
                    {
                        vm.ShortList.Add(new JobApplicationShortListViewModel()
                        {
                            ApplicationIID = detailDat.ApplicationIID,
                            JobID = detailDat.JobID,
                            AppliedDateString = detailDat.AppliedDateString,
                            ApplicantName = detailDat.ApplicantName,
                            Education = detailDat.Education,
                            TotalYearOfExperience = detailDat.TotalYearOfExperience,
                            CVContentID = detailDat.CVContentID,
                            Remarks = detailDat.Remarks,
                            IsShortListed = detailDat.IsShortListed,
                        });
                    }
                }
            }
            return vm;
        }
    }
}
