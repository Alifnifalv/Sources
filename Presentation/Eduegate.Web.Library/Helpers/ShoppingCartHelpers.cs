using System;
using System.Linq;
using Eduegate.Framework;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.ShoppingCart;
using Eduegate.Web.Library.ViewModels.ShoppingCart;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Services.Client.Factory;

namespace Eduegate.Web.Library.Helpers
{
    public class ShoppingCartHelpers 
    {
        private CallContext _context;
        public ShoppingCartHelpers(CallContext context)
        {
            _context = context;
        }
        public AddToCartStatusDTO AddToCart(CartProductViewModel product)
        {
            var productDTO = new CartProductDTO();
            var result = new AddToCartStatusDTO();
            try
            {
                productDTO.SKUID = product.SKU;
                productDTO.Quantity = product.Quantity;
                productDTO.CustomerID = product.CustomerID;
                result = ClientFactory.ShoppingCartServiceClient(_context, null, null).AddToCart(productDTO);
            }
            catch (Exception ex)
            {
                Eduegate.Logger.LogHelper<ShoppingCartHelpers>.Fatal(ex.Message.ToString(), ex);
                result.Status = false;
            }
            return result;
        }


        public CartDTO GetCartPrice(long customerID=0)
        {
            return ClientFactory.ShoppingCartServiceClient(_context, null, null).GetCartPrice(customerID);           
        }

        public bool UpdateCartDelivery(long SKUID, Int16 deliveryTypeID, long? customerID = null)
        {
            var product = new CartProductDTO(); 
            product.DeliveryTypeID = deliveryTypeID;
            product.SKUID = SKUID;
            product.CustomerID = customerID;
            return ClientFactory.ShoppingCartServiceClient(_context, null, null).UpdateCartDelivery(product);
        }

        public bool UpdateCart(long SKUID, decimal quantity,long customerID = 0)
        {
            var currentInventoryBranchList = ClientFactory.ProductCatalogServiceClient(_context, null).GetProductInventoryOnline(SKUID);
            var currentInventoryBranch = (from productInventoryDetails in currentInventoryBranchList
                                          where productInventoryDetails.ProductSKUMapID == SKUID
                                          select productInventoryDetails).FirstOrDefault();
            if (quantity > Convert.ToInt64(currentInventoryBranch.Quantity))
            {
                quantity = currentInventoryBranch.Quantity;
            }

            var product = new CartProductDTO();
            product.Quantity = quantity;
            product.SKUID = SKUID;
            product.BranchID = currentInventoryBranch.BranchID;
            product.CustomerID = customerID;
            return ClientFactory.ShoppingCartServiceClient(_context, null, null).UpdateCart(product);
        }

        public bool RemoveItem(long SKUID, long customerID = 0)
        {
            return ClientFactory.ShoppingCartServiceClient(_context, null, null).RemoveItem(SKUID,customerID);
        }
    }
}
