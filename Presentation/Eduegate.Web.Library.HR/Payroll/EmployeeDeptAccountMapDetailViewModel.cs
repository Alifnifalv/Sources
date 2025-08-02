using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.HR.Payroll
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "EmployeeDeptAccountMapDetail", "CRUDModel.ViewModel.EmployeeDeptAccountMapDetail")]
    [DisplayName(" ")]
    public class EmployeeDeptAccountMapDetailViewModel : BaseMasterViewModel
    {
        public EmployeeDeptAccountMapDetailViewModel()
        {
            StaffLedgerAccount = new KeyValueViewModel();
            ExpenseLedgerAccount= new KeyValueViewModel();
            ProvisionLedgerAccount= new KeyValueViewModel();
        }
        public long EmployeeDepartmentAccountMaplIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown)]
        [LookUp("LookUps.Department")]
        [CustomDisplay("Department")]
        public string Department { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("StaffLedgerAccounts", "Numeric", false, "")]
        [LookUp("LookUps.StaffLedgerAccounts")]
        [CustomDisplay("Staff Ledger Account")]
        public KeyValueViewModel StaffLedgerAccount { get; set; }
        public long? StaffLedgerAccountID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ExpenseLedgerAccounts", "Numeric", false, "")]
        [LookUp("LookUps.ExpenseLedgerAccounts")]
        [CustomDisplay("Expense Ledger Account")]
        public KeyValueViewModel ExpenseLedgerAccount { get; set; }
        public long? ExpenseLedgerAccountID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [Select2("ProvisionLedgerAccounts", "Numeric", false, "")]
        [LookUp("LookUps.ProvisionLedgerAccounts")]
        [CustomDisplay("Provision Ledger Account")]
        public KeyValueViewModel ProvisionLedgerAccount { get; set; }
        public long? ProvisionLedgerAccountID { get; set; }
        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Select2)]
        //[Select2("TaxLedgerAccounts", "Numeric", false, "")]
        //[LookUp("LookUps.TaxLedgerAccounts")]
        //[CustomDisplay("Tax Ledger Account")]
        //public KeyValueViewModel TaxLedgerAccount { get; set; }
        //public long? TaxLedgerAccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index,ModelStructure.EmployeeDeptAccountMapDetail[0],CRUDModel.ViewModel.EmployeeDeptAccountMapDetail)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='RemoveGridRow($index,ModelStructure.EmployeeDeptAccountMapDetail[0],CRUDModel.ViewModel.EmployeeDeptAccountMapDetail)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
