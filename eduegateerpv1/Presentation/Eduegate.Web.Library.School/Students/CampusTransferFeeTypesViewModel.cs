using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Web.Library.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Eduegate.Framework.Translator;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.School.Students;
using Eduegate.Web.Library.School.Common;
using Eduegate.Services.Contracts.School.Common;
using System.Globalization;
using Eduegate.Web.Library.Common;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Web.Library.School.Students
{
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "FeeTypes", "CRUDModel.ViewModel.FeeTypes")]
    [DisplayName("FeeTypes")]
    public class CampusTransferFeeTypeViewModel : BaseMasterViewModel
    {
        public CampusTransferFeeTypeViewModel()
        {
            //IsRowSelected = true;
            IsFeePeriodDisabled = true;
            MontlySplitMaps = new List<CampusTransferMontlySplitMapsViewModel>() { new CampusTransferMontlySplitMapsViewModel() };
        }

        [ControlType(Framework.Enums.ControlTypes.ExpandCollapseButton, "small-col-width")]
        [DisplayName(" ")]
        public bool IsExpand { get; set; }

        public bool? IsTutionFee { get; set; }
        public bool? IsTransportFee { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "small-col-width", Attributes = "ng-change=ClearSelection(gridModel)")]
        //[CustomDisplay("Selection")]
        //public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceNo")]
        public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("InvoiceDate")]
        public string InvoiceDateString { get; set; }
        public DateTime? InvoiceDate { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("Fee")]
        public string FeeMaster { get; set; }

        public int? FeeMasterID { get; set; }

        public bool IsFeePeriodDisabled { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("FeePeriod")]
        public string FeePeriod { get; set; }
        public int? FeePeriodID { get; set; }


        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("From Campus Due")]
        public decimal? FromCampusDue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("To Campus Due")]
        public decimal? ToCampusDue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("CreditNote")]
        public decimal? CreditNoteAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("PrvCollected")]
        public decimal? PrvCollect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [CustomDisplay("Receivable")]
        public decimal? ReceivableAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [MaxLength(15)]
        [CustomDisplay("Payable")]
        public decimal? PayableAmount { get; set; }

        public long? FeeDueFeeTypeMapsID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.GridGroup, "feeStructure", Attributes2 = "colspan=12", Attributes4 = "ng-hide=(gridModel.FeePeriodID==0)")]
        [DisplayName("")]
        public List<CampusTransferMontlySplitMapsViewModel> MontlySplitMaps { get; set; }
    }
        


    [ContainerType(Framework.Enums.ContainerTypes.Grid, "MontlySplitMaps", "gridModel.MontlySplitMaps", gridBindingPrefix: "MontlySplitMaps")]
    [DisplayName("Split")]

    public class CampusTransferMontlySplitMapsViewModel : BaseMasterViewModel
    {

        public long? FeeDueMonthlySplitID { get; set; } 
        public int? MonthID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "textleft", Attributes = "ng-change=UpdateParentFee(CRUDModel.ViewModel.FeeTypes[$parent.$index],$index,this)")]
        //[CustomDisplay("Select")]
        //public bool? IsRowSelected { get; set; }

        [ControlType(Framework.Enums.ControlTypes.HiddenWithLabel)]
        [DisplayName("")]
        public string space { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label)]
        [CustomDisplay("Month")]
        public string MonthName { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("From Campus Due")]
        public decimal? FromCampusDue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "large-col-width")]
        [CustomDisplay("To campus Due")]
        public decimal? ToCampusDue { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("CreditNote")]
        public decimal? CreditNoteAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("PrvCollected")]
        public decimal? PrvCollect { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Recievable")]
        public decimal? ReceivableAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [CustomDisplay("Payable")]
        public decimal? PayableAmount { get; set; }

    }
}