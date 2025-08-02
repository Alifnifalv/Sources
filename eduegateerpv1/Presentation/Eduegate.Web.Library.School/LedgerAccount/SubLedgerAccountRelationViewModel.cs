using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;

namespace Eduegate.Web.Library.School.LedgerAccount
{
    public class SubLedgerAccountRelationViewModel : BaseMasterViewModel
    {
        //[ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind='gridModel.Key'")]
        //[DisplayName("Accounts")]
        //public string Key { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, Attributes = "ng-bind='gridModel.Value'")]
        //public string Value { get; set; }


        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        public long SL_Rln_ID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        public long AccountID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        public long SL_AccountID { get; set; }

    }
}