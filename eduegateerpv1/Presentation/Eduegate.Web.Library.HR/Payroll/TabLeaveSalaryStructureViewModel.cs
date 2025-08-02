using Eduegate.Domain.Entity.HR.Payroll;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabLeaveSalaryStructure", "CRUDModel.ViewModel.MCTabLeaveSalaryStructure")]
    [DisplayName("Leave Salary Structure")]
    public class TabLeaveSalaryStructureViewModel : BaseMasterViewModel
    {
        public TabLeaveSalaryStructureViewModel()
        {
           
            SalaryComponents = new List<EmployeeSalaryStructureComponentViewModel>() { new EmployeeSalaryStructureComponentViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Salary Structure")]
        public List<EmployeeSalaryStructureComponentViewModel> SalaryComponents { get; set; }
    }
}
