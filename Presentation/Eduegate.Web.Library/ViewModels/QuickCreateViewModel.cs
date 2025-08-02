using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class QuickCreateViewModel
    {
        public long ProductIID { get; set; }

        [Required (ErrorMessage = "ProductName is required")]
        public string ProductName { get; set; }
        public string ProductGroup { get; set; }
        public Nullable<long> BrandIID { get; set; }
        public KeyValueViewModel Brand { get; set; }
        public Nullable<long> StatusIID { get; set; }
        public Nullable<long> ProductFamilyIID { get; set; }
        public KeyValueViewModel ProductFamily { get; set; }
        public Nullable<long> ProductOwnderID { get; set; }
        public decimal Price { get; set; }
        public string PriceUnit { get; set; }
        public string KeyWords { get; set; }
        public Nullable<bool> IsOnline { get; set; }
        public List<CultureDataInfoViewModel> CultureInfo { get; set; }
        public List<ProductPropertiesTypeValueViewModel> Properties { get; set; }
        public List<PropertyViewModel> DefaultProperties { get; set; }

        public ProductInventoryConfigViewModel ProductInventoryConfigViewModels { get; set; }
        public List<DeliveryTypeViewModel> DeliveryTypeViewModels { get; set; }
        public List<PackingTypeViewModel> PackingTypes{ get; set; }
        public List<ProductDeliveryCountrySettingViewModel> Countries { get; set; }
        public Nullable<long> ProductTypeID { get; set; }
        public string StatusName { get; set; }
        //public KeyValueViewModel ProductType { get; set; }
        public string TaxTemplate { get; set; }
    }
}