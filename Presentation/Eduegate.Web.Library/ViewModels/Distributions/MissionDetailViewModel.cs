using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Distributions
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class MissionDetailViewModel : BaseMasterViewModel
    {
        public MissionDetailViewModel()
        {
           
        }

        public long JobEntryHeadID { get; set; }
        [ControlType(Framework.Enums.ControlTypes.RowIndicator, "ex-small-col-width statuscolumn")]
        [Order(0)]
        [DisplayName("")]
        public string RowIndicator { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [Order(1)]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }

        public long JobEntryDetailIID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width serialnum-wrap")]
        [Order(2)]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [Order(3)]
        [DisplayName("Job ID")]
        public string JobID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [Order(4)]
        [DisplayName("Job Number")]
        public string JobNumber { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        //[Order(5)]
        //[DisplayName("Invoice No")]
        //public string InvoiceNo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [Order(5)]
        [DisplayName("Cart ID")]
        public string ShoppingCartID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [Order(6)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width textright")]
        [Order(7)]
        [DisplayName("City")]
        public string CityName { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBoxWithButton, "ex-large-col-width mediumtextbox gridfield-popupwrap", " ng-disabled= !CRUDModel.Model.MasterViewModel.IsServiceProver ",
            "", "ng-click='ShowShipment(detail, $event);'", false, "ng-click='PrintAWBPDF(detail);'", "ng-click='ShowShipmentStatus(detail);'")]
        [Order(8)]
        [DisplayName("AWB No")]
        public string AWBNo { get; set; }
    }
}
