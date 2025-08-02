using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
using Eduegate.Services.Contracts.School.Forms;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "JobDescription", "CRUDModel.ViewModel")]
    [DisplayName("")]
    public class JobDescriptionViewModel : BaseMasterViewModel
    {
        public JobDescriptionViewModel()
        {
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            JobDescriptionDetail = new List<JobDescriptionDetailsViewModel>() { new JobDescriptionDetailsViewModel() };
            JDDateString = DateTime.Now.ToString(dateFormat, CultureInfo.InvariantCulture);
        }
        public long JDMasterIID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Date")]
        public string JDDateString { get; set; }
        public System.DateTime? JDDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Title")]
        public string Title { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; } 

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("JD Ref")]
        public string JDReference { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [CustomDisplay("Rev Ref")]
        public string RevReference { get; set; }  
        
        
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("Rev Date")]
        public string RevDateString { get; set; }
        public System.DateTime? RevDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine6 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Designation")]
        [CustomDisplay("Designation")]
        public string Designation { get; set; }

        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Department")]
        [DisplayName("Department")]
        public string Department { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=ActiveEmployees", "LookUps.ActiveEmployees")]
        [CustomDisplay("Reporting To")]
        public KeyValueViewModel ReportingToEmployee { get; set; }
        public long? ReportingToEmployeeID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea,"w-100")]
        [CustomDisplay("Role Summary")]
        public string RoleSummary { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea,"w-100")]
        [CustomDisplay("Responsibilities")]
        public string Responsibilities { get; set; }

        public string Undertaking { get; set; } 


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Job Description")]
        public List<JobDescriptionDetailsViewModel> JobDescriptionDetail { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as JobDescriptionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobDescriptionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<JobDescriptionDTO, JobDescriptionViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var frmDto = dto as JobDescriptionDTO;
            var vm = Mapper<JobDescriptionDTO, JobDescriptionViewModel>.Map(frmDto);

            vm.JDDateString = (frmDto.JDDate.HasValue ? frmDto.JDDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.RevDateString = (frmDto.RevDate.HasValue ? frmDto.RevDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.ReportingToEmployee = KeyValueViewModel.ToViewModel(frmDto.ReportingToEmployee);

            vm.JDReference = frmDto.JDReference;
            vm.RevReference = frmDto.RevReference;
            vm.Title = frmDto.Title;
            vm.RoleSummary = frmDto.RoleSummary;
            vm.Undertaking = frmDto.Undertaking;
            vm.Responsibilities = frmDto.Responsibilities;

            vm.Department = frmDto.DepartmentID.ToString();
            vm.Designation = frmDto.DesignationID.ToString();

            vm.JobDescriptionDetail = new List<JobDescriptionDetailsViewModel>();
            foreach (var detail in frmDto.JDDetail)
            {
                vm.JobDescriptionDetail.Add(new JobDescriptionDetailsViewModel()
                {
                    JDMapID = detail.JDMapID,
                    JDMasterID = detail.JDMasterID,
                    Description = detail.Description,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<JobDescriptionViewModel, JobDescriptionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            var dto = Mapper<JobDescriptionViewModel, JobDescriptionDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.JDMasterIID = this.JDMasterIID;
            dto.Title = this.Title;
            dto.JDDate = string.IsNullOrEmpty(this.JDDateString) ? (DateTime?)null : DateTime.ParseExact(this.JDDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.RevDate = string.IsNullOrEmpty(this.RevDateString) ? (DateTime?)null : DateTime.ParseExact(this.RevDateString, dateFormat, CultureInfo.InvariantCulture);

            dto.ReportingToEmployeeID = string.IsNullOrEmpty(this.ReportingToEmployee.Key) ? (long?)null : long.Parse(this.ReportingToEmployee.Key);
            dto.ReportingToEmployee = new KeyValueDTO { Key = this.ReportingToEmployee.Key, Value = this.ReportingToEmployee.Value };

            dto.DepartmentID = string.IsNullOrEmpty(this.Department) ? (long?)null : long.Parse(this.Department);
            dto.DesignationID = string.IsNullOrEmpty(this.Designation) ? (int?)null : int.Parse(this.Designation);

            dto.RoleSummary = this.RoleSummary;
            dto.Undertaking = this.Undertaking;
            dto.Responsibilities = this.Responsibilities;

            dto.JDDetail = new List<JobDescriptionDTO.JobDescriptionDetailDTO>();

            foreach (var detail in this.JobDescriptionDetail)
            {
                if (detail.Description != null)
                {
                    dto.JDDetail.Add(new JobDescriptionDTO.JobDescriptionDetailDTO()
                    {
                       JDMapID = detail.JDMapID,
                       JDMasterID = detail.JDMasterID,
                       Description = detail.Description,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<JobDescriptionDTO>(jsonString);
        }

    }
}