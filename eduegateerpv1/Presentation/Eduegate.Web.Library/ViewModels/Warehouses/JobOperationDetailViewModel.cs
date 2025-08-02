using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Mvc.Attributes;

namespace Eduegate.Web.Library.ViewModels.Warehouses
{
    [Order(1)]
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "Model.DetailViewModel")]
    public class JobOperationDetailViewModel : BaseMasterViewModel
    {
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

        public Nullable<long> ProductSKUID { get; set; }

        public long TransactionDetailID { get; set; }
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [Order(2)]
        [DisplayName("Sr.No")]
        public long SerialNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "small-col-width")]
        [Order(4)]
        [DisplayName("Part No")]
        public string PartNo { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [Order(4)]
        [DisplayName("Description")]
        public string Description { get; set; }        

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Image, "medium-col-width thumbnailview")]
        [Order(5)]
        [DisplayName("Product Image")]
        public string ProductImage { get; set; }

        public Nullable<decimal> Quantity { get; set; }

        public Nullable<int> LocationID { get; set; }

        public Nullable<decimal> UnitPrice { get; set; }

        public string LocationBarcode { get; set; }

        public string BarCode { get; set; }

        public Nullable<decimal> Price { get; set; }
        
        public Nullable<bool> IsVerifyQuantity { get; set; }

        public Nullable<bool> IsVerifyBarCode { get; set; }

        public Nullable<bool> IsVerifyLocation { get; set; }

        public Nullable<bool> IsVerifyPartNo { get; set; }

        public Nullable<int> JobStatusID { get; set; }

        public string Remarks { get; set; }
    }
}
