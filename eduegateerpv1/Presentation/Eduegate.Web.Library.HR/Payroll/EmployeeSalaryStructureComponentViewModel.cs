using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "SalaryComponents", "CRUDModel.ViewModel.MasterSalaryComponents.SalaryComponents")]
    [DisplayName("Components")]
    public class EmployeeSalaryStructureComponentViewModel : BaseMasterViewModel
    {
        public EmployeeSalaryStructureComponentViewModel()
        {
            SalaryComponent = new KeyValueViewModel();
        }

        public long EmployeeSalaryStructureComponentMapIID { get; set; }

        public long? EmployeeSalaryStructureID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryComponent", "Numeric", false, "")]
        [LookUp("LookUps.SalaryComponent")]
        [CustomDisplay("Components")]
        public KeyValueViewModel SalaryComponent { get; set; }
        public int? SalaryComponentID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Earnings")]
        public decimal? Earnings { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(15, ErrorMessage = "Maximum Length should be within 15!")]
        [CustomDisplay("Deduction")]
        public decimal? Deduction { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.SalaryComponents[0], CRUDModel.ViewModel.MasterSalaryComponents.SalaryComponents)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index, ModelStructure.SalaryComponents[0],CRUDModel.ViewModel.MasterSalaryComponents.SalaryComponents)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}