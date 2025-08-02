using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Web.Library.School.Accounts
{
    public class LedgerEntryViewModel
    {
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Account ID")]
        public long AccountID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Account Code")]
        [MaxLength(30, ErrorMessage = "Maximum Length should be within 30!")]

        public string AccountCode { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Alias")]
        //[MaxLength(30, ErrorMessage = "Maximum Length should be within 30!")]
        //public string Alias { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [DisplayName("Account Name")]
        [MaxLength(50, ErrorMessage = "Maximum Length should be within 30!")]
        public string AccountName { get; set; }
        public long? ParentAccountID { get; set; }
        public long? AccountGroupID { get; set; }

        public bool? IsCreateLedger { get; set; }
    //[Required]
    //[ControlType(Framework.Enums.ControlTypes.Select2)]
    //[DisplayName("Account Groups")]
    //[Select2("AccountGroups", "Numeric", false)]
    //[LookUp("LookUps.AccountGroup")]
    //[QuickSmartView("AccountGroups")]
    //public KeyValueViewModel Group { get; set; }
    //public AccountBehavior AccountBehavior { get; set; }

    //[Required]
    //[ControlType(Framework.Enums.ControlTypes.DropDown)]
    //[DisplayName("Behaviour")]
    //[LookUp("LookUps.AccountBehavior")]
    //public string Behavior { get; set; }

}
}
