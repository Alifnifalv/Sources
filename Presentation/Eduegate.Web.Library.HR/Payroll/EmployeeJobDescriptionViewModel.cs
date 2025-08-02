using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.HR.Payroll;
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
    //Screen For Employee or Applicant JD review and Changes

    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmployeeJobDescription", "CRUDModel.ViewModel")]
    [DisplayName("")]
    public class EmployeeJobDescriptionViewModel : BaseMasterViewModel
    {
        public EmployeeJobDescriptionViewModel()
        {
            EmpJobDescriptionDetail = new List<EmployeeJobDescriptionDetailsViewModel>() { new EmployeeJobDescriptionDetailsViewModel() };
            JDDate = DateTime.Now;
        }

        public long JobDescriptionIID { get; set; }
        public long? JobApplicationID { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("Employee", "Numeric", false, "getEmployeeBasicDetails($event, $element, CRUDModel.ViewModel)")]
        //[LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=ActiveEmployees", "LookUps.ActiveEmployees")]
        //[CustomDisplay("Employee")]
        //public KeyValueViewModel Employee { get; set; }
        //public long? EmployeeID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Applicant Name")]
        public string ApplicantName { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.HtmlLabel)]
        [CustomDisplay("IsAgreementSigned")]
        public string IsAgreementSigned { get; set; } 
        
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Agreement Signed Date")]
        public string AgreementSignedDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        //[DisplayName("")]
        //public string NewLine0 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("Designation")]
        //public string Designation { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("Department")]
        //public string Department { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label)]
        //[CustomDisplay("QID No.")]
        //public string QID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("JD Ref")]
        public string JDReference { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker, attribs: "ng-disabled=true")]
        [CustomDisplay("Date")]
        public string JDDateString { get; set; }
        public System.DateTime? JDDate { get; set; }


        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[CustomDisplay("Rev Ref")]
        public string RevReference { get; set; }  
        
        
        //[ControlType(Framework.Enums.ControlTypes.DatePicker)]
        //[CustomDisplay("Rev Date")]
        public string RevDateString { get; set; }
        public System.DateTime? RevDate { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", false, "")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=ActiveEmployees", "LookUps.ActiveEmployees")]
        [CustomDisplay("Reporting To")]
        public KeyValueViewModel ReportingToEmployee { get; set; }
        public long? ReportingToEmployeeID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.CheckBox)]
        [CustomDisplay("IsActive")]
        public bool? IsActive { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea,"w-100")]
        [CustomDisplay("Role Summary")]
        public string RoleSummary { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextArea,"w-100")]
        [CustomDisplay("Undertaking")]
        public string Undertaking { get; set; } 


        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Job Description")]
        public List<EmployeeJobDescriptionDetailsViewModel> EmpJobDescriptionDetail { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as EmployeeJobDescriptionDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeJobDescriptionViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<EmployeeJobDescriptionDTO, EmployeeJobDescriptionViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();

            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");
            var frmDto = dto as EmployeeJobDescriptionDTO;
            var vm = Mapper<EmployeeJobDescriptionDTO, EmployeeJobDescriptionViewModel>.Map(frmDto);

            vm.JobDescriptionIID = frmDto.JobDescriptionIID;
            vm.JobApplicationID = frmDto.JobApplicationID;
            vm.JDDateString = (frmDto.JDDate.HasValue ? frmDto.JDDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.RevDateString = (frmDto.RevDate.HasValue ? frmDto.RevDate.Value : DateTime.Now).ToString(dateFormat, CultureInfo.InvariantCulture);
            vm.AgreementSignedDate = frmDto.AgreementSignedDate.HasValue ? frmDto.AgreementSignedDate.Value.ToString(dateFormat, CultureInfo.InvariantCulture) : null;
            //vm.Employee = KeyValueViewModel.ToViewModel(frmDto.Employee);
            vm.ReportingToEmployee = KeyValueViewModel.ToViewModel(frmDto.ReportingToEmployee);
            vm.ApplicantName = frmDto.ApplicantName;
            vm.JDReference = frmDto.JDReference;
            vm.RevReference = frmDto.RevReference;
            vm.IsAgreementSigned = frmDto.IsAgreementSigned == true ? "YES" : "NO";
            //vm.Department = frmDto.Department;
            //vm.Designation = frmDto.Designation;
            //vm.QID = frmDto.QID;
            vm.RoleSummary = frmDto.RoleSummary;
            vm.Undertaking = frmDto.Undertaking;
            vm.IsActive = frmDto.IsActive;

            vm.EmpJobDescriptionDetail = new List<EmployeeJobDescriptionDetailsViewModel>();
            foreach (var detail in frmDto.EmpJobDescriptionDetail)
            {
                vm.EmpJobDescriptionDetail.Add(new EmployeeJobDescriptionDetailsViewModel()
                {
                    JobDescriptionMapID = detail.JobDescriptionMapID,
                    JobDescriptionID = detail.JobDescriptionID,
                    Description = detail.Description,
                });
            }

            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<EmployeeJobDescriptionViewModel, EmployeeJobDescriptionDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();

            var dto = Mapper<EmployeeJobDescriptionViewModel, EmployeeJobDescriptionDTO>.Map(this);
            var dateFormat = new Domain.Setting.SettingBL().GetSettingValue<string>("DateFormat");

            dto.JobDescriptionIID = this.JobDescriptionIID;
            dto.JobApplicationID = this.JobApplicationID;
            dto.JDDate = string.IsNullOrEmpty(this.JDDateString) ? (DateTime?)null : DateTime.ParseExact(this.JDDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.RevDate = string.IsNullOrEmpty(this.RevDateString) ? (DateTime?)null : DateTime.ParseExact(this.RevDateString, dateFormat, CultureInfo.InvariantCulture);
            dto.AgreementSignedDate = string.IsNullOrEmpty(this.AgreementSignedDate) ? (DateTime?)null : DateTime.ParseExact(this.AgreementSignedDate, dateFormat, CultureInfo.InvariantCulture);
            dto.IsAgreementSigned = this.IsAgreementSigned == "YES" ? true : false;
            //dto.EmployeeID = string.IsNullOrEmpty(this.Employee.Key) ? (long?)null : long.Parse(this.Employee.Key);
            //dto.Employee = new KeyValueDTO { Key = this.Employee.Key, Value = this.Employee.Value };
            dto.ReportingToEmployeeID = string.IsNullOrEmpty(this.ReportingToEmployee.Key) ? (long?)null : long.Parse(this.ReportingToEmployee.Key);
            dto.ReportingToEmployee = new KeyValueDTO { Key = this.ReportingToEmployee.Key, Value = this.ReportingToEmployee.Value };
            dto.IsActive = this.IsActive;
            dto.RoleSummary = this.RoleSummary;
            dto.Undertaking = this.Undertaking;

            dto.EmpJobDescriptionDetail = new List<EmployeeJobDescriptionDTO.EmployeeJobDescriptionDetailDTO>();

            foreach (var detail in this.EmpJobDescriptionDetail)
            {
                if (detail.Description != null)
                {
                    dto.EmpJobDescriptionDetail.Add(new EmployeeJobDescriptionDTO.EmployeeJobDescriptionDetailDTO()
                    {
                       JobDescriptionMapID = detail.JobDescriptionMapID,
                       JobDescriptionID = detail.JobDescriptionID,
                       Description = detail.Description,
                    });
                }
            }

            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<EmployeeJobDescriptionDTO>(jsonString);
        }

    }
}