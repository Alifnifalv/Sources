using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.Common;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Eduegate.Web.Library.School.Fees
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeFines", "CRUDModel.ViewModel.FeeFines")]
    [DisplayName("FeeFines")]
    public class FeeCollectionFineViewModel : BaseMasterViewModel
    {
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceNo")]
        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceDate")]
        public string InvoiceDateString { get; set; }
        public DateTime? InvoiceDate { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[DisplayName("Fine Applied Date")]
        //public string FineMapDateString { get; set; }
        //public DateTime? FineMapDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Fine")]
        public string Fine { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Amount")]
        public decimal? Amount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Credit/DebitNote")]
        public decimal? CreditNote { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("PrvCollected")]
        public decimal? PrvCollect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("NowPaying")]
        public decimal? NowPaying { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Balance")]
        public decimal? Balance { get; set; }

        public int? FineMasterID { get; set; }
        public long? FineMasterStudentMapID { get; set; }

        public long FeeCollectionFeeTypeMapsIID { get; set; }
        public long? FeeDueFeeTypeMapsID { get; set; }
        public long? StudentFeeDueID { get; set; }
        public string FineMapDateString { get; set; }
        public string FineMapDate { get; set; }
    }
}
