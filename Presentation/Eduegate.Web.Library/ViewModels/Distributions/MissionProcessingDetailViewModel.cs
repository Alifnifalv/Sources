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
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "Model.DetailViewModel")]
    public class MissionProcessingDetailViewModel : BaseMasterViewModel
    {
        public long HeadID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.RowIndicator, "ex-small-col-width statuscolumn")]
        [Order(0)]
        [DisplayName("")]
        public string RowIndicator { get; set; }

        [ControlType(Framework.Enums.ControlTypes.CheckBox, "ex-small-col-width", "ng-model='detail.IsRowSelected'")]
        [Order(1)]
        [DisplayName("IsRowSelected")]
        public bool IsRowSelected { get; set; }
        public bool SelectAll { get; set; }

        public Nullable<long> ProductSKUID { get; set; }

        public long TransactionDetailID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [Order(2)]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "ex-large-col-width")]
        [Order(3)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Image, "medium-col-width serialnum-wrap thumbnailview")]
        [Order(4)]
        [DisplayName("Product Image")]
        public string ProductImage { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width serialnum-wrap right-align")]
        [Order(5)]
        [DisplayName("Quantity")]
        public Nullable<decimal> Quantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width serialnum-wrap")]
        [Order(6)]
        [DisplayName("Amount")]
        public Nullable<decimal> Amount { get; set; }

        public long LocationID { get; set; }

        public string BarCode { get; set; }

        public bool IsQuantiyVerified { get; set; }

        public bool IsBarCodeVerified { get; set; }

        public bool IsLocationVerified { get; set; }

        public long JobStatusID { get; set; }

        public decimal ValidatedQuantity { get; set; }

        public string ValidatedLocationBarcode { get; set; }

        public string ValidatedPartNo { get; set; }

        public string ValidationBarCode { get; set; }

        public string Remarks { get; set; }
    }
}
