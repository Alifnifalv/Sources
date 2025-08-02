using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SalaryComponents", "CRUDModel.ViewModel.MCTabSalaryStructure.SalaryComponents")]
    [DisplayName("Components")]
    public class EmployeeSettlementComponentsViewModel : BaseMasterViewModel
    {
        public EmployeeSettlementComponentsViewModel()
        {
            SalaryComponent = new KeyValueViewModel();
        }
       
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryComponent", "Numeric", false, "")]
        [LookUp("LookUps.SalaryComponent")]
        [CustomDisplay("Components")]
        public KeyValueViewModel SalaryComponent { get; set; }
        public int? SalaryComponentID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Earnings")]
        public decimal? Earnings { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Deduction")]
        public decimal? Deduction { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("No.of Days")]
        public decimal? NoOfDays { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea, attribs: "ng-disabled=true")]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}