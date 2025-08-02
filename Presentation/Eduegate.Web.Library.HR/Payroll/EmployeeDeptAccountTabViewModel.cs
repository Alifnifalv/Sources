using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.School.Fees;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "EmpDepartmentAccountMap", "CRUDModel.ViewModel.EmpDepartmentAccountMap")]
    //[Pagination(10, "default")]
    public class EmployeeDeptAccountTabViewModel : BaseMasterViewModel
    {
        public EmployeeDeptAccountTabViewModel()
        {
            EmployeeDeptAccountMapDetail = new List<EmployeeDeptAccountMapDetailViewModel>() { new EmployeeDeptAccountMapDetailViewModel() };
        }
        [ControlType(Framework.Enums.ControlTypes.Grid)]
        [CustomDisplay("Details")]
        public List<EmployeeDeptAccountMapDetailViewModel> EmployeeDeptAccountMapDetail { get; set; }
    }
}
