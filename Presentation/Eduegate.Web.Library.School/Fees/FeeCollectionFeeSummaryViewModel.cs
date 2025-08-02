using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeSummary", "CRUDModel.ViewModel.FeeSummary")]
    [DisplayName("FeeSummary")]
    public  class FeeCollectionFeeSummaryViewModel : BaseMasterViewModel
    {
        // [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Due Amount")]
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


        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long? FeeStructureFeeMapID { get; set; }

        public long? CreditNoteFeeTypeMapID { get; set; }

    }
}
