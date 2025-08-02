using Newtonsoft.Json;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeDueMonthly", "gridModel.FeeDueMonthly",
        gridBindingPrefix: "FeeDueMonthly")]
    [DisplayName("Split")]
    public class FeeDueMonthlySplitViewModel : BaseMasterViewModel
    {
        public long MapIID { get; set; }

        public long? FeeStructureMontlySplitMapID { get; set; }
        public long? ParentID { get; set; }

        public int MonthID { get; set; }
        public int Year { get; set; }
        public int? FeePeriodID { get; set; }
        public decimal? CreditNoteAmount { get; set; }

        public bool? FeeCollectionStatus { get; set; }

        public long? FeeDueMonthlySplitID { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [DisplayName("Month")]
        public string MonthName { get; set; }
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Fee Due Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Credit/Debit Note")]
        public decimal? CreditNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Prv Collected")]
        public decimal? PrvCollect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Now Paying")]
        public decimal? NowPaying { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Balance")]
        public decimal? Balance { get; set; }

    }
}


