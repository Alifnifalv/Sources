using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Supports
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "1", "CRUDModel.ViewModel.ActionTab")]
    [DisplayName("Refund")]
    public class ActionTabViewModel : BaseMasterViewModel
    {
        public ActionTabViewModel()
        {
            CollectItem = new CollectItemViewModel();
            DirectReplacement = new DirectReplacementViewModel();
            Arrangement = new ArrangementViewModel();
            DigitalCard = new DigitalCardViewModel();
            AmountCapture = new AmountCaptureViewModel();
        }

        [ControlType(Framework.Enums.ControlTypes.Select2)]
        [DisplayName("Refund Type")]
        [Select2("RefundType", "Numeric", false)]
        [LookUp("LookUps.RefundType")]
        public KeyValueViewModel RefundType { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Refund Amount")]
        public decimal? RefundAmount { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(250, ErrorMessage = "Maximum Length should be within 250!")]
        [DisplayName("Reason")]
        public string Reason { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextArea)]
        [MaxLength(500, ErrorMessage = "Maximum Length should be within 500!")]
        [DisplayName("Remarks")]
        public string Remarks { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(20, ErrorMessage = "Maximum Length should be within 20!")]
        [DisplayName("Return Number")]
        public string ReturnNumber { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "2", "CRUDModel.ViewModel.ActionTab.RefundDetails")]
        [DisplayName("Collect Item")]
        public CollectItemViewModel CollectItem { get; set; }


        [ContainerType(Framework.Enums.ContainerTypes.Tab, "3", "CRUDModel.ViewModel.ActionTab.DirectReplacement")]
        [DisplayName("Direct Replacement")]
        public DirectReplacementViewModel DirectReplacement { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "4", "CRUDModel.ViewModel.ActionTab.Arrangement")]
        [DisplayName("Arrangement")]
        public ArrangementViewModel Arrangement { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "5", "CRUDModel.ViewModel.ActionTab.DigitalCard")]
        [DisplayName("Digital Card")]
        public DigitalCardViewModel DigitalCard { get; set; }

        [ContainerType(Framework.Enums.ContainerTypes.Tab, "6", "CRUDModel.ViewModel.ActionTab.AmountCapture")]
        [DisplayName("Amount Capture")]
        public AmountCaptureViewModel AmountCapture { get; set; }

        //[ContainerType(Framework.Enums.ContainerTypes.Tab, "RefundDetails1", "Ref1")]
        //[DisplayName("Documents")]
        //public RefundDetailViewModel RefundDetail1 { get; set; }

        public long ActionDetailMapIID { get; set; }
    }
}
