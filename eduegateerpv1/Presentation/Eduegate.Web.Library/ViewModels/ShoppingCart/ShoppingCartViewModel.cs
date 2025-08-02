using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Web.Library.ViewModels.Catalog;

namespace Eduegate.Web.Library.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        public List<ShoppingCartCatalogList> ProductDetails { get; set; }

        public string SubTotal { get; set; }

        public string Total { get; set; }

        public bool IsVoucherApplied { get; set; }

        public string VoucherValue { get; set; }

        public long TransactionHeadId { get; set; }

        public string PaymentMethod { get; set; }

        public bool IsDeliveryCharge { get; set; }
        public string DeliveryCharge { get; set; }

        public string ParentPage { get; set; }
        public bool IsCartItemDeleted { get; set; }
        public bool IsCartItemOutOfStock { get; set; }
        public bool IsCartItemQuantityAdjusted { get; set; }
        public bool IsProceedToPayment { get; set; }
        public bool IsIntlCart { get; set; }
        public string DisplayText { get; set; }



        public static ShoppingCartViewModel ToViewModel(CartDTO dto)
        {
            var ShoppingCartViewModel = new ShoppingCartViewModel();

            ShoppingCartViewModel.IsProceedToPayment = true;
            if (dto != null && dto.Products != null)
            {
                ShoppingCartViewModel.ProductDetails = new List<ShoppingCartCatalogList>();
                foreach (var product in dto.Products)
                {
                    var ShoppingCartCatalogList = new ShoppingCartCatalogList();
                    ShoppingCartCatalogList.ProductID = product.SKUID;
                    ShoppingCartCatalogList.ProductName = product.ProductName;
                    ShoppingCartCatalogList.ProductDiscountPrice = Convert.ToDecimal(product.DiscountedPrice);
                    ShoppingCartCatalogList.ProductPrice = Convert.ToDecimal(product.Price);
                    ShoppingCartCatalogList.ProductAvailableQuantity = Convert.ToInt32(product.AvailableQuantity);
                    ShoppingCartCatalogList.ProductCartQuantity = Convert.ToInt32(product.Quantity);
                    ShoppingCartCatalogList.DeliveryMethodSelected = Convert.ToInt32(product.DeliveryTypeID);
                    ShoppingCartCatalogList.IsOutOfStock = product.IsOutOfStock;
                    ShoppingCartCatalogList.IsCartQuantityAdjusted = product.IsCartQuantityAdjusted;
                    ShoppingCartCatalogList.DeliveryOptions = new List<KeyValueViewModel>();
                    if (product.DeliveryTypes != null)
                    {
                        foreach (var delivery in product.DeliveryTypes)
                        {
                            var deliveryOptions = new KeyValueViewModel();
                            deliveryOptions.Key = delivery.DeliveryMethodID.ToString();
                            deliveryOptions.Value = delivery.DeliveryType;
                            ShoppingCartCatalogList.DeliveryOptions.Add(deliveryOptions);
                        }
                    }
                    if (ShoppingCartCatalogList.DeliveryMethod.Count == 0)
                    {
                        ShoppingCartViewModel.IsProceedToPayment = false;
                    }
                    ShoppingCartViewModel.ProductDetails.Add(ShoppingCartCatalogList);
                }
                //subtotal and total
                ShoppingCartViewModel.SubTotal = dto.SubTotal;
                ShoppingCartViewModel.Total = dto.Total;
                ShoppingCartViewModel.IsVoucherApplied = dto.IsVoucherApplied;
                ShoppingCartViewModel.VoucherValue = dto.VoucherValue;
                ShoppingCartViewModel.IsDeliveryCharge = dto.IsDeliveryCharge;
                ShoppingCartViewModel.DeliveryCharge = Convert.ToString(dto.DeliveryCharge);
                ShoppingCartViewModel.IsCartItemDeleted = dto.IsCartItemDeleted;
                ShoppingCartViewModel.IsCartItemOutOfStock = dto.IsCartItemOutOfStock;
                ShoppingCartViewModel.IsCartItemQuantityAdjusted = dto.IsCartItemQuantityAdjusted;
            }
            return ShoppingCartViewModel;

        }
    }
}