using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeMonthly", "gridModel.FeeMonthly",
        gridBindingPrefix: "splitMonthly")]
    [DisplayName("Split")]
    public class FeeMonthlySplitViewModel : BaseMasterViewModel
    {
        public long MapIID { get; set; }

        public long? FeeMasterClassMapID { get; set; }
        public long? ParentID { get; set; }

        public int MonthID { get; set; }

        public int Year { get; set; }

        public int? FeePeriodID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Month")]
        public string MonthName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "textright", optionalAttribs: "ng-disabled=true")]
        [DisplayName("Amount")]
        public decimal? Amount { get; set; }
    }
}

