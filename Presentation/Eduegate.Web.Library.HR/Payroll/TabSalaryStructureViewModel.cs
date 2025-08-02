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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "MCTabSalaryStructure", "CRUDModel.ViewModel.MCTabSalaryStructure")]
    [DisplayName("Salary Structure")]
    public class TabSalaryStructureViewModel : BaseMasterViewModel
    {
        public TabSalaryStructureViewModel()
        {
            SalaryComponents = new List<EmployeeSettlementComponentsViewModel>() { new EmployeeSettlementComponentsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Salary Structure")]
        public List<EmployeeSettlementComponentsViewModel> SalaryComponents { get; set; }
    }
}
