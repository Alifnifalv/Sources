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
    [ContainerType(Framework.Enums.ContainerTypes.Grid, "ProductDetails", "CRUDModel.ViewModel.ProductMap.ProductDetails")]
    [DisplayName("")]
    public class DeliveryTypeProductDetailViewModelcs : BaseMasterViewModel
    {
        public long ProductDeliveryTypeMapIID { get; set; }

        public long DelivertyTypeID { get; set; }

        public long ProductID { get; set; }

        public long ProductSKUMapID { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width")]
        [Select2("SKUID", "String", false)]
        [DisplayName("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        public KeyValueViewModel SKUID { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Cart Total From")]
        //public Nullable<int> CartTotalLimitFrom { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.TextBox)]
        //[DisplayName("Cart Total To")]
        //public Nullable<int> CartTotalLimitTo { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(8, ErrorMessage = "Maximum Length should be within 8!")]
        [DisplayName("Delivery Charge")]
        public Nullable<decimal> DeliveryCharge { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox)]
        [MaxLength(7, ErrorMessage = "Maximum Length should be within 7!")]
        [DisplayName("Delivery Charge %")]
        public Nullable<decimal> DeliveryChargePercentage { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='InsertGridRow($index, ModelStructure.ProductMap.ProductDetails[0], CRUDModel.ViewModel.ProductMap.ProductDetails)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.Button, "", "ng-click='RemoveGridRow($index, ModelStructure.ProductMap.ProductDetailss[0], CRUDModel.ViewModel.ProductMap.ProductDetails)'")]
        [DisplayName("-")]
        public string Remove { get; set; }
    }
}
