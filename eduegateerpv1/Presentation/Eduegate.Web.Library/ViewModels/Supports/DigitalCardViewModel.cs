using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "5", "CRUDModel.ViewModel.ActionTab.DigitalCard")]
    [DisplayName("Digital Card")]
    public class DigitalCardViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Issue Type")]
        [Select2("IssueType", "Numeric", false)]
        [LookUp("LookUps.IssueType")]
        public KeyValueViewModel IssueType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [LookUp("LookUps.Employees")]
        [Select2("Employees", "Numeric", false)]
        [DisplayName("Employees")]
        //public List<KeyValueViewModel> Employees { get; set; }
        public KeyValueViewModel Employees { get; set; }


        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2, "onecol-header-left")]
        //[LazyLoad("", "Employee/GetEmployeesByRoleID?roleID=3", "LookUps.Customer")]
        //[Select2("Customer", "Numeric", false)]
        //[DisplayName("Customer")]
        //[QuickSmartView("Customer")]
        //[QuickCreate("Customer")]
        //public KeyValueViewModel Customer { get; set; }
    }
}
