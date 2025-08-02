using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Web.Library.Common;
using System.Globalization;

namespace Eduegate.Web.Library.School.Fees


{    

    [ContainerType(Framework.Enums.ContainerTypes.Grid, "DetailData", "CRUDModel.ViewModel.DetailData")]
    [DisplayName("FeeDetail")]
    public class CollectFeeAccountPostingDetailViewModel : BaseMasterViewModel
    {
        public CollectFeeAccountPostingDetailViewModel()
        {           
            SplitData = new List<CollectFeeAccountPostingSplitViewModel>() { new CollectFeeAccountPostingSplitViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        //[DisplayName("Particulars")]
        //public string Particulars { get; set; }
        [ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", attribs: "ng-disabled=true")]
        [CustomDisplay("Is Posted")]
        public bool? IsPosted { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Collection Date")]
        public string CollectionDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Receipt No.")]
        public string ReceiptNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Group Trans. No.")]
        public string GroupTransactionNumber { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Admission No.")]
        public string Student { get; set; }
        public long StudentId { get; set; }

        public byte PaymentType { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, " ")]
        //[DisplayName(" ")]
        //public string Newline1 { get; set; }
        //[ControlType(Framework.Enums.ControlTypes.Label, " ")]
        //[DisplayName(" ")]
        //public string Newline2 { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Cash")]
        public decimal? CashAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Card")]
        public decimal? CardAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Cheque")]
        public decimal? Cheque { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Online-Bank")]
        public decimal? BankAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Online-Direct")]
        public decimal? OnlineDirectAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Others")]
        public decimal? OtherAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes2 = "colspan=9")]
        [DisplayName(" ")]
        public List<CollectFeeAccountPostingSplitViewModel> SplitData { get; set; }
    }


}
