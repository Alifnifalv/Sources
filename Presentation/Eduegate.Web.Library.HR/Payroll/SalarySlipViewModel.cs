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
    public class SalarySlipViewModel : BaseMasterViewModel
    {
        public SalarySlipViewModel()
        {
            //Department = new List<KeyValueViewModel>();
            Employee = new List<KeyValueViewModel>();
            SlipComponents = new List<SalarySlipComponentViewModel>() { new SalarySlipComponentViewModel() };
        }

        
         public long SalarySlipIID { get; set; }
        
        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine1 { get; set; }
        
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DatePicker)]
        [CustomDisplay("SlipDate")]
        public string SlipDateString { get; set; }
        public DateTime? SlipDate { get; set; }

       
       
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Department", "Numeric", true,"")]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Department")]
        public List<KeyValueViewModel> Department { get; set; }
        //public long? DepartmentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine3 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("Employee", "Numeric", true, "")]
        //[LookUp("LookUps.Employee")]
        [LazyLoad("", "Mutual/GetDynamicLookUpData?lookType=ActiveEmployees", "LookUps.ActiveEmployees")]
        [CustomDisplay("EmployeeName")]
        public List<KeyValueViewModel> Employee { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft")]
        [CustomDisplay("Generate Leave/Vacation Salary")]
        public bool? IsGenerateLeaveOrVacationsalary { get; set; }

        public bool IsRegenerate { get; set; } = false;

        [ControlType(Framework.Enums.ControlTypes.NewLine, "fullwidth alignleft")]
        [DisplayName("")]
        public string NewLine2 { get; set; }

      

        [ControlType(Framework.Enums.ControlTypes.Button,Attributes = "ng-Click=GenerateSalarySlip(CRUDModel.ViewModel.Department,0)")]
        [CustomDisplay("Generates")]
        public string GenerateButton { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, Attributes = "ng-Click=GenerateSalarySlip(CRUDModel.ViewModel.Department,1)")]
        [CustomDisplay("Regenerates")]
        public string ReGenerateButton { get; set; }

        public long? EmployeeID { get; set; }
        public List<SalarySlipComponentViewModel> SlipComponents { get; set; }

        public override string AsDTOString(BaseMasterDTO vm)
        {
            return JsonConvert.SerializeObject(vm as SalarySlipDTO);
        }

            
        public override BaseMasterDTO ToDTO()
        {
            Mapper<SalarySlipViewModel, SalarySlipDTO>.CreateMap();
            Mapper<KeyValueViewModel, KeyValueDTO>.CreateMap();
            var dto = Mapper<SalarySlipViewModel, SalarySlipDTO>.Map(this);

            dto.SlipDate = string.IsNullOrEmpty(this.SlipDateString) ? (DateTime?)null : DateTime.Parse(SlipDateString);

            List<KeyValueDTO> employeeList = new List<KeyValueDTO>();

            foreach (KeyValueViewModel em in this.Employee)
            {
                employeeList.Add(new KeyValueDTO { Key = em.Key, Value = em.Value }
                    );
            }
            dto.Employees = employeeList;

            List<KeyValueDTO> departmentlist = new List<KeyValueDTO>();
            foreach(KeyValueViewModel vm in this.Department)
            {
                departmentlist.Add(new KeyValueDTO { Key = vm.Key, Value = vm.Value }
                    );
            }
            dto.Department = departmentlist;
            return dto;
        }

        public override BaseMasterViewModel ToVM(string jsonString)
        {
            return JsonConvert.DeserializeObject<SalarySlipViewModel>(jsonString);
        }

    }
}
