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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "SalarySlipView", "CRUDModel.ViewModel")]
    [DisplayName("Salary Slip Publish")]
    public class SalarySlipMasterViewModel : BaseMasterViewModel
    {
        public SalarySlipMasterViewModel()
        {
            EmployeeList = new List<SalarySlipEmployeeViewModel>() { new SalarySlipEmployeeViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Months", "Numeric", false, "", false)]
        [CustomDisplay("Month")]
        [LookUp("LookUps.Months")]
        public KeyValueViewModel Months { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Years", "Numeric", false, "", false)]
        [CustomDisplay("Year")]
        [LookUp("LookUps.Years")]
        public KeyValueViewModel Years { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine4 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Department", "Numeric", false, "DepartmentChange($event, $element, CRUDModel.ViewModel)")]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Department")]
        public KeyValueViewModel Department { get; set; }
        public int? DepartmentID { get; set; }


        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("VerificationStatus", "Numeric", false)]
        [LookUp("LookUps.VerificationStatusID")]
        [CustomDisplay("Verification Status")]
        public KeyValueViewModel VerificationStatus { get; set; }
        public int? VerificationStatusID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HtmlLabel, Attributes2 = "htmllabelinfo")]
        [CustomDisplay("Working Days in this month")]
        public string TotalWorkingDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine5 { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", attribs: "ng-click=\"PublishSalarySlip()\"")]
        [CustomDisplay("Publish")]
        public string PublishButton { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", attribs: "ng-click=\"PrintSlipReport()\"")]
        [CustomDisplay("Print")]
        public string PrintButton { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", attribs: "ng-click=\"MailReport()\"")]
        [CustomDisplay("Email")]
        public string EmailButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("EmployeeList")]
        public List<SalarySlipEmployeeViewModel> EmployeeList { get; set; }



        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalarySlipMasterDTO);
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalarySlipMasterViewModel>(jsonString);
        }

        public override BaseMasterViewModel ToVM(BaseMasterDTO dto)
        {
            Mapper<SalarySlipMasterDTO, SalarySlipMasterViewModel>.CreateMap();
            Mapper<KeyValueDTO, KeyValueViewModel>.CreateMap();
            var hlthdto = dto as SalarySlipMasterDTO;
            var vm = Mapper<SalarySlipMasterDTO, SalarySlipMasterViewModel>.Map(hlthdto);
            vm.DepartmentID = hlthdto.DepartmentID;
            vm.Months.Key = Convert.ToString(hlthdto.Month);
            vm.Years.Key = Convert.ToString(hlthdto.Year);
            vm.EmployeeList = new List<SalarySlipEmployeeViewModel>();
            foreach (var studMap in hlthdto.SalarySlipEmployee)
            {
                vm.EmployeeList.Add(new SalarySlipEmployeeViewModel()
                {
                    EmployeeID = studMap.EmployeeID,
                    EmployeeName = studMap.EmployeeName,
                    IsSelected = studMap.IsSelected,
                    EmailAddress = studMap.EmailAddress,
                });
            }
            return vm;
        }

        public override BaseMasterDTO ToDTO()
        {
            Mapper<SalarySlipMasterViewModel, SalarySlipMasterDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SalarySlipMasterViewModel, SalarySlipMasterDTO>.Map(this);
            dto.DepartmentID = string.IsNullOrEmpty(this.Department.Key) ? (int?)null : int.Parse(this.Department.Key);
            dto.SalarySlipEmployee = new List<SalarySlipEmployeeDTO>();
            foreach (var EmployeeList in this.EmployeeList)
            {
                if (EmployeeList.EmployeeID.HasValue)
                {
                    dto.SalarySlipEmployee.Add(new SalarySlipEmployeeDTO()
                    {
                        EmployeeID = EmployeeList.EmployeeID,
                        EmployeeName = EmployeeList.EmployeeName,
                        IsSelected = EmployeeList.IsSelected,
                        EmailAddress = EmployeeList.EmailAddress,
                    });
                }
            }
            return dto;
        }

        public override BaseMasterDTO ToDTO(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalarySlipMasterDTO>(jsonString);
        }

    }
}
