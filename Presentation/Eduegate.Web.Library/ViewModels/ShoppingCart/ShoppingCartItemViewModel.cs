using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Mvc.Attributes;
using Eduegate.Framework.Translator;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
    [ContainerType(Framework.Enums.ContainerTypes.Tab, "", "CRUDModel.Model.DetailViewModel")]
    public class ShoppingCartItemViewModel : BaseMasterViewModel
    {
        public ShoppingCartItemViewModel()
        {
            //Unit = "Pcs";
            DeliveryTypes = new List<DeliveryTypeViewModel>();
        }

        public string PriceUnit { get; set; }

        public string SubTotal { get; set; }

        public string Details { get; set; }

        public string Size { get; set; }

        public decimal AllowedQuantity { get; set; }

        public string Branch { get; set; }

        public double Total { get; set; } 
         
        [Required]
        [ControlType(Framework.Enums.ControlTypes.Select2, "large-col-width detailview-grid")]
        [Select2("SKUID", "String", false, "OnSKUChange($select,$index, detail,CRUDModel.Model.MasterViewModel.Customer.Key)")]
        [DisplayName("Product")]
        [LazyLoad("", "Catalogs/ProductSKU/ProductSKUSearch", "LookUps.ProductSKUs")]
        [QuickSmartView("Catalogs/ProductSKU", "SKU Details")]
        public KeyValueViewModel SKUID { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width")]
        [DisplayName("Available Quantity")]
        public decimal AvailableQuantity { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Quantity")]
        public double Quantity { get; set; }

        public bool IsEdit { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='UpdateItem($index,CRUDModel.Model.DetailViewModel[$index],CRUDModel.Model.DetailViewModel[$index].Quantity,true,CRUDModel.Model.MasterViewModel.Customer.Key)' ng-show='(CRUDModel.Model.DetailViewModel[$index].Quantity > 1)'")]
        [DisplayName("Update")] 
        public string Update { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.Label, "medium-col-width", "initialvalue='Pcs'")]
        //[DisplayName("Unit")]
        //public string Unit { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width")]
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

        [Required]
        [ControlType(Framework.Enums.ControlTypes.DropDown, "medium-col-width", "ng-change='DeliverySelectionChanged(CRUDModel.Model.DetailViewModel[$index].SKUID.Key,CRUDModel.Model.DetailViewModel[$index].DeliveryOption,CRUDModel.Model.MasterViewModel.Customer.Key)'")]
        [LookUp("detail.LookUps.DeliveryOptions")]
        [DisplayName("Delivery Option")]
        public string DeliveryOption { get; set; }

        public List<KeyValueViewModel> DeliveryOptions { get; set; }

        //[Required]
        //[ControlType(Framework.Enums.ControlTypes.TextBox, "medium-col-width textright")]
        //[DisplayName("Delivery Charge")]
        public decimal DeliveryCharge { get; set; }

        [ControlType(Framework.Enums.ControlTypes.TextBox, "ex-small-col-width")]
        [DisplayName("Amount")]
        public double Amount { get; set; }  

        [ControlType(Framework.Enums.ControlTypes.Button, "ex-small-col-width", "ng-click='RemoveItem($index, CRUDModel.Model.DetailViewModel[$index], CRUDModel.Model.MasterViewModel.Customer.Key)' ng-show='(CRUDModel.Model.DetailViewModel[$index].Quantity >= 1)'")]
        [DisplayName("Remove")]
        public string Remove { get; set; }

        [ControlType(Framework.Enums.ControlTypes.Button, "small-col-width", "ng-click='InsertGridRow($index, ModelStructure.Model.DetailViewModel[0], CRUDModel.Model.DetailViewModel)'")]
        [DisplayName("+")]
        public string Add { get; set; }

        public long UnitID { get; set; }

        public Int32 DeliveryTypeID { get; set; }

        public Nullable<decimal> MinimumQuanityInCart { get; set; }
        public Nullable<decimal> MaximumQuantityInCart { get; set; }
        public List<KeyValuePair<byte, string>> Properties { get; set; }
        public List<KeyValuePair<long, string>> Categories { get; set; }

        public List<DeliveryTypeViewModel> DeliveryTypes { get; set; }
        public bool IsOutOfStock { get; set; }

        public bool IsCartQuantityAdjusted { get; set; }

        //[ControlType(Framework.Enums.ControlTypes.CheckBox, "medium-col-width textright")]
        //[DisplayName("Serial No")]
        public bool IsSerialNo { get; set; }

        public long BranchID { get; set; }

        public int DeliveryPriority { get; set; }

        public static List<CartProductDTO> ToDTOList(List<ShoppingCartItemViewModel> vmList)
        {
            var dtoList = new List<CartProductDTO>();
            foreach (var item in vmList)
            {

                if (item.SKUID != null && !string.IsNullOrEmpty(item.SKUID.Key))
                {
                    var dto = new CartProductDTO();
                    dto.SKUID = long.Parse(item.SKUID.Key);
                    dto.Quantity = decimal.Parse(item.Quantity.ToString());
                    dto.ProductDiscountPrice = decimal.Parse(item.UnitPrice.ToString());
                    dto.DeliveryTypeID = item.DeliveryTypeID;
                    dtoList.Add(dto);
                }
            }
            return dtoList;
        }


        public static ShoppingCartItemViewModel ToViewModel(CartDTO dto)
        {
            var vm1 = new ShoppingCartItemViewModel();
            vm1.DeliveryOptions = new List<KeyValueViewModel>();
            if (dto.IsNotNull())
            {

                if (dto.Products.IsNotNull() && dto.Products.Count > 0)
                {
                    foreach (var item in dto.Products)
                    {
                        vm1.Description = item.ProductName;
                        vm1.AvailableQuantity = item.AvailableQuantity;
                        vm1.Quantity = Convert.ToDouble(item.Quantity);
                        vm1.UnitPrice = Convert.ToDouble(item.PriceUnit);
                        vm1.DeliveryCharge = dto.DeliveryCharge;
                        foreach (var dt in item.DeliveryTypes)
                        {
                            var deliveryOptions = new KeyValueViewModel();
                            deliveryOptions.Key = dt.DeliveryMethodID.ToString();
                            deliveryOptions.Value = dt.DeliveryType;
                            vm1.DeliveryOptions.Add(deliveryOptions);                          
                        }
                        vm1.Total = Convert.ToDouble(dto.Total);
                        vm1.SubTotal = dto.SubTotal;
                    }
                }
                return vm1;

            }
            else
                return new ShoppingCartItemViewModel();
        }
    }
}
