using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MasterLeaveSalaryComponents", "CRUDModel.ViewModel.MasterLeaveSalaryComponents")]
    [DisplayName("Salary Components")]
    public class EmployeeLeaveSalaryComponentsViewModel : BaseMasterViewModel
    {
        public EmployeeLeaveSalaryComponentsViewModel()
        {
            LeaveSalaryComponents = new List<EmployeeLeaveSalaryStructureComponentViewModel>() { new EmployeeLeaveSalaryStructureComponentViewModel() };
            SalaryStructure = new KeyValueViewModel();
        }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("SalaryStructure", "Numeric", false, "LeaveSalaryStructureChanges($event, $element, CRUDModel.ViewModel.MasterLeaveSalaryComponents)")]
        [LookUp("LookUps.SalaryStructure")]
        [CustomDisplay("SalaryStructureName")]
        public KeyValueViewModel SalaryStructure { get; set; }
        public long? SalaryStructureID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Components")]
        public List<EmployeeLeaveSalaryStructureComponentViewModel> LeaveSalaryComponents { get; set; }

    }
}