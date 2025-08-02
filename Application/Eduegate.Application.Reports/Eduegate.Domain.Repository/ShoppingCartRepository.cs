using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Extensions;
using Eduegate.Framework.Helper.Enums;
using Eduegate.Services.Contracts.Eduegates;
using Eduegate.Framework.Services;
using Eduegate.Framework.Enums;
using System.Data.Entity;
using Eduegate.Services.Contracts.Catalog;
using System.Data;
using System.Data.SqlClient;
using Eduegate.Framework;
using Eduegate.Domain.Entity.Models.ValueObjects;
using Eduegate.Services.Contracts.Checkout;
using Eduegate.Services.Contracts;
using System.Runtime.Remoting.Contexts;
using Eduegate.Services.Contracts.Mutual;
using Eduegate.Services.Contracts.School.Students;

namespace Eduegate.Domain.Repository
{
    public class ShoppingCartRepository
    {

        #region Shopping cart methods

        public List<CartProductDTO> GetCartItems(string customerID, long shoppingCartID, ShoppingCartStatus? shoppingCartStatusID = null, int cultureID = 1, CallContext callContext = null)
        {
            var cartItems = new List<CartProductDTO>();
            var cartItemList = ShoppingCartItems(shoppingCartID, (long)shoppingCartStatusID, cultureID, callContext);

            if (cartItemList != null && cartItemList.AsDataView().ToTable().Rows.Count > 0)
            {
                foreach (DataRow drItems in cartItemList.AsDataView().ToTable().Rows)
                {
                    cartItems.Add(new CartProductDTO
                    {
                        SKU = Convert.ToString(drItems["SKU"]),
                        SKUID = Convert.ToInt64(drItems["SKUID"]),
                        ProductID = Convert.ToInt64(drItems["ProductID"]),
                        ProductName = Convert.ToString(drItems["ProductName"]),
                        ImageFile = Convert.ToString(drItems["ImageFile"]),
                        AvailableQuantity = Convert.ToInt64(drItems["AvailableQuantity"]),
                        AllowedQuantity = Convert.ToInt64(drItems["AllowedQuantity"]),
                        Price = drItems["Price"] is DBNull ? 0 : Convert.ToDecimal(drItems["Price"]),
                        Quantity = Convert.ToDecimal(drItems["Quantity"]),
                        DiscountedPrice = drItems["DiscountedPrice"] is DBNull ? null : Convert.ToString(drItems["DiscountedPrice"]),
                        MinimumQuanityInCart = drItems["MinimumQuanityInCart"] is DBNull ? 0 : Convert.ToDecimal(drItems["MinimumQuanityInCart"]),
                        MaximumQuantityInCart = Convert.ToDecimal(drItems["MaximumQuantityInCart"]),
                        DeliveryTypeID = Convert.ToInt32(drItems["DeliveryTypeID"]),
                        BranchID = Convert.ToInt32(drItems["BranchID"]),
                        Total = Convert.ToString(drItems["Total"]),
                        DeliveryPriority = Convert.ToInt32(drItems["Priority"]),
                        DeliveryDays = drItems.Table.Columns.Contains("DeliveryDays") ? Convert.ToInt32(drItems["DeliveryDays"]) : (int?)null,
                        DeliveryRange = drItems.Table.Columns.Contains("DisplayRange") ? Convert.ToInt32(drItems["DisplayRange"]) : (int?)null
                    });
                }
            }
            return cartItems;
        }

        public List<KeyValuePair<byte, string>> GetPropertiesBySKU(long skuMapId)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();


            var query = (from ppm in db.ProductPropertyMaps
                         join prop in db.Properties on ppm.PropertyID equals prop.PropertyIID
                         join pt in db.PropertyTypes on ppm.PropertyTypeID equals pt.PropertyTypeID
                         where ppm.ProductSKUMapID == skuMapId
                         select new
                         {
                             pt.PropertyTypeID,
                             prop.PropertyName
                         })
                         .AsEnumerable().Select(o => new KeyValuePair<byte, string>((byte)o.PropertyTypeID, o.PropertyName))
                         //.ToDictionary(o => (byte)o.PropertyTypeID, o => o.PropertyName)
                         ;
            return query.ToList();
        }


        public List<KeyValuePair<long, string>> GetCategoriesBySKU(long skuMapId)
        {
            dbEduegateERPContext db = new dbEduegateERPContext();
            var query = (from psm in db.ProductSKUMaps
                         join pcm in db.ProductCategoryMaps on psm.ProductID equals pcm.ProductID
                         join c in db.Categories on pcm.CategoryID equals c.CategoryIID
                         where psm.ProductSKUMapIID == skuMapId && c.ParentCategoryID != null
                         select new
                         {
                             c.CategoryIID,
                             c.CategoryName
                         })
                         .AsEnumerable().Select(o => new KeyValuePair<long, string>(o.CategoryIID, o.CategoryName))
                         //.ToDictionary(o => o.CategoryIID, o => o.CategoryName)
                         ;
            return query.ToList();
        }



        public ShoppingCart GetCartDetail(string customerID, int shoppingCartStatusID, int? siteID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var carts = (from sc in db.ShoppingCarts
                             where sc.CartID == customerID
                             && sc.CartStatusID == shoppingCartStatusID && sc.SiteID == siteID
                             select sc).OrderByDescending(a => a.ShoppingCartIID);

                if (carts.Count() <= 1)
                {
                    return carts.FirstOrDefault();
                }
                else
                {
                    carts.ToList().ForEach(x => x.CartStatusID = (int)ShoppingCartStatus.Invalid);
                    db.SaveChanges();
                }
                return null;
            }
        }

        public Entity.Models.Login GetShoppingCartLogin(long cartID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from cart in db.ShoppingCarts
                        join log in db.Logins on cart.CreatedBy equals log.LoginIID
                        where cart.ShoppingCartIID == cartID
                        select log).FirstOrDefault();

            }
        }

        public Entity.Models.Status GetCartStatus(int shoppingCartStatusID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from sc in db.ShoppingCarts
                        join ts in db.Statuses on sc.CartStatusID equals ts.StatusID
                        where sc.CartStatusID == shoppingCartStatusID
                        select ts).FirstOrDefault();
            }
        }

        public ShoppingCart GetCartDetailbyIID(long cartIID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from sc in db.ShoppingCarts
                        where sc.ShoppingCartIID == cartIID
                        select sc).FirstOrDefault();
            }
        }

        public List<ShoppingCartItem> GetCartItemsBycartID(long cartID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from sc in db.ShoppingCarts
                        join scitm in db.ShoppingCartItems on
                        sc.ShoppingCartIID equals scitm.ShoppingCartID
                        where sc.ShoppingCartIID == cartID
                        select scitm).ToList();

            }
        }

        public ShoppingCart GetCartDetail(string customerID, long TransactionHeadId, int shoppingCartStatusID)
        {
            var cart = new ShoppingCart();
            int? cartStatusID = null;
            if (shoppingCartStatusID.IsNotNull())
                cartStatusID = (int)shoppingCartStatusID;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                cart = (from sc in db.ShoppingCarts
                        join tsm in db.TransactionHeadShoppingCartMaps on sc.ShoppingCartIID equals tsm.ShoppingCartID
                        where sc.CartID == customerID
                        && sc.CartStatusID == shoppingCartStatusID
                        && tsm.TransactionHeadID == TransactionHeadId
                        select sc).FirstOrDefault();
            }
            return cart;
        }

        public ShoppingCart AddCart(ShoppingCart userCart)
        {
            using (var db = new dbEduegateERPContext())
            {
                db.ShoppingCarts.Add(userCart);
                db.SaveChanges();
            }
            return userCart;
        }

        public void SetCartStatusInvalid(string customerID, int shoppingCartStatusID, int? siteID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.ShoppingCarts
                       .Where(x => x.CartID == customerID && x.SiteID == siteID)
                       .ToList()
                       .ForEach(x => x.CartStatusID = (int)ShoppingCartStatus.Invalid);
                db.SaveChanges();
            }
        }

        public bool AddItem(ShoppingCartItem item)
        {
            var result = false;
            using (var db = new dbEduegateERPContext())
            {
                db.ShoppingCartItems.Add(item);
                var queryResult = db.SaveChanges();
                if (queryResult > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool UpdateItem(long SKUID, decimal quantity, string customerId, long BranchID, int? siteID)
        {
            var result = false;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    ShoppingCartItem cartItem = (from sc in dbContext.ShoppingCarts
                                                 join sci in dbContext.ShoppingCartItems on sc.ShoppingCartIID equals sci.ShoppingCartID
                                                 where sci.ProductSKUMapID == SKUID && sc.CartID == customerId && sc.CartStatusID == (int)ShoppingCartStatus.InProcess && sc.SiteID == siteID
                                                 select sci).FirstOrDefault();

                    if (cartItem.IsNotNull())
                    {
                        cartItem.Quantity = quantity;
                        cartItem.BranchID = BranchID;
                        cartItem.UpdatedDate = DateTime.Now;
                        var queryResult = dbContext.SaveChanges();

                        if (queryResult > 0)
                            result = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCartRepository>.Fatal(exception.Message, exception);
            }
            return result;
        }

        public bool UpdateItemPrice(long SKUID, string customerId, decimal ProductPrice, decimal ProductDiscountPrice, int? siteID)
        {
            var result = false;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    ShoppingCartItem cartItem = (from sc in dbContext.ShoppingCarts
                                                 join sci in dbContext.ShoppingCartItems on sc.ShoppingCartIID equals sci.ShoppingCartID
                                                 where sci.ProductSKUMapID == SKUID && sc.CartID == customerId && sc.CartStatusID == (int)ShoppingCartStatus.InProcess && sc.SiteID == siteID
                                                 select sci).FirstOrDefault();

                    if (cartItem.IsNotNull())
                    {
                        cartItem.ProductPrice = ProductPrice;
                        cartItem.ProductDiscountPrice = ProductDiscountPrice;
                        cartItem.UpdatedDate = DateTime.Now;
                        var queryResult = dbContext.SaveChanges();

                        if (queryResult > 0)
                            result = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCartRepository>.Fatal(exception.Message, exception);
            }
            return result;
        }

        public bool UpdateItemDelivery(long SKUID, int deliveryTypeID,
            string customerId, int? siteID, int? DeliveryDays, int? DisplayRange, int? deliveryTimeSlotMapID)
        {
            var result = false;
            try
            {
                using (var dbContext = new dbEduegateERPContext())
                {
                    if (SKUID != 0)
                    {
                        var cart = (from sc in dbContext.ShoppingCarts
                                    where
                                    sc.CartID == customerId &&
                                    sc.CartStatusID == (int)ShoppingCartStatus.InProcess
                                    && sc.SiteID == siteID
                                    select sc).FirstOrDefault();

                        var cartItem = (from sc in dbContext.ShoppingCarts
                                        join sci in dbContext.ShoppingCartItems
                                        on sc.ShoppingCartIID equals sci.ShoppingCartID
                                        where sci.ProductSKUMapID == SKUID &&
                                        sc.CartID == customerId &&
                                        sc.CartStatusID == (int)ShoppingCartStatus.InProcess
                                        && sc.SiteID == siteID
                                        select sci).FirstOrDefault();

                        if (cartItem.IsNotNull())
                        {
                            cart.DeliveryDaysCount = 1;

                            cartItem.DeliveryTypeID = deliveryTypeID;
                            cartItem.UpdatedDate = DateTime.Now;
                            cartItem.DeliveryDays = DeliveryDays;
                            cartItem.DisplayRange = DisplayRange;
                            dbContext.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                            var queryResult = dbContext.SaveChanges();
                            if (queryResult > 0)
                                result = true;
                        }
                    }
                    else
                    {
                        var cart = (from sc in dbContext.ShoppingCarts
                                    where
                                    sc.CartID == customerId &&
                                    sc.CartStatusID == (int)ShoppingCartStatus.InProcess
                                    && sc.SiteID == siteID
                                    select sc).FirstOrDefault();

                        if (cart.IsNotNull())
                        {
                            cart.DeliveryTypeID = deliveryTypeID;
                            cart.UpdatedDate = DateTime.Now;
                            cart.DeliveryDays = DeliveryDays;
                            cart.DisplayRange = DisplayRange;
                            cart.DeliveryTimeslotID = deliveryTimeSlotMapID;
                            cart.DeliveryDaysCount = 1;
                            cart.SubscriptionTypeID = null;
                            cart.StartDate = null;
                            cart.EndDate = null;

                            dbContext.Entry(cart).State = System.Data.Entity.EntityState.Modified;

                            foreach (var cartItem in cart.ShoppingCartItems)
                            {
                                cartItem.DeliveryTypeID = deliveryTypeID;
                                cartItem.UpdatedDate = DateTime.Now;
                                cartItem.DeliveryDays = DeliveryDays;
                                cartItem.DisplayRange = DisplayRange;
                                dbContext.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                            }

                            var queryResult = dbContext.SaveChanges();
                            if (queryResult > 0)
                                result = true;
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCartRepository>.Fatal(exception.Message, exception);
            }

            return result;
        }

        public bool RemoveVoucherMap(long shoppingCartVoucherMapID)
        {
            var result = false;
            //var db = new dbEduegateERPContext();
            var voucherMap = new ShoppingCartVoucherMap();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                voucherMap = (from scvm in db.ShoppingCartVoucherMaps
                              where scvm.ShoppingCartVoucherMapID == shoppingCartVoucherMapID
                              select scvm).FirstOrDefault();

                db.ShoppingCartVoucherMaps.Remove(voucherMap);
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public ShoppingCartVoucherMap AddVoucherMap(ShoppingCartVoucherMap voucherMap)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.ShoppingCartVoucherMaps.Add(voucherMap);
                db.SaveChanges();
                return voucherMap;
            }

        }

        public ShoppingCartVoucherMap UpdateVoucherMap(ShoppingCartVoucherMap voucherMap)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                ShoppingCartVoucherMap cartVoucherMap = db.ShoppingCartVoucherMaps.Where(x => x.ShoppingCartVoucherMapID == voucherMap.ShoppingCartVoucherMapID).FirstOrDefault();
                cartVoucherMap.Amount = voucherMap.Amount;
                db.SaveChanges();
                return voucherMap;
            }
        }

        public ShoppingCart IsUserCartExist(string customerID, int cartStatus, int? siteID)
        {
            var userCart = new ShoppingCart();

            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                userCart = (from u in dbContext.ShoppingCarts
                            where u.CartID == customerID && u.CartStatusID == cartStatus && u.SiteID == siteID
                            select u).FirstOrDefault(); //sigleOrdefault gives sequence contains more than one element so changed to first or default
            }

            return userCart;
        }

        public decimal GetShoppingCartItemQuantity(long skuID, string cartID)
        {
            var item = new ShoppingCartItem();
            int cartInProcessStatusId = (int)ShoppingCartStatus.InProcess;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                item = (from u in dbContext.ShoppingCartItems
                        join c in dbContext.ShoppingCarts on u.ShoppingCartID equals c.ShoppingCartIID
                        where u.ProductSKUMapID == skuID && c.CartID == cartID && c.CartStatusID == cartInProcessStatusId
                        select u).FirstOrDefault();
            }
            return decimal.Parse(item.Quantity.ToString());
        }

        public ShoppingCartItem IsItemExist(long skuID, string cartID)
        {
            var item = new ShoppingCartItem();
            int cartInProcessStatusId = (int)ShoppingCartStatus.InProcess;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                item = (from u in dbContext.ShoppingCartItems
                        join c in dbContext.ShoppingCarts on u.ShoppingCartID equals c.ShoppingCartIID
                        where u.ProductSKUMapID == skuID && c.CartID == cartID && c.CartStatusID == cartInProcessStatusId
                        select u).FirstOrDefault();
            }

            return item;
        }

        public bool IsDigitalProduct(long skuID, string cartID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                if ((from sku in dbContext.ProductSKUMaps
                     join prd in dbContext.Products on sku.ProductID equals prd.ProductIID
                     where sku.ProductSKUMapIID == skuID && prd.ProductTypeID == (int)ProductTypes.Digital
                     select sku).Any())
                    return true;
                else
                    return false;
            }
        }

        public bool MergeCart(string guid, string customerId, int? siteID = 0)
        {
            var result = false;
            var item = new ShoppingCart();
            using (var db = new dbEduegateERPContext())
            {
                // if customerId exist in ShoppingCarts table with status Inprocess(9)
                var existCart = IsUserCartExist(customerId, (int)ShoppingCartStatus.InProcess, siteID);

                // get the ShoppingCarts based on GUID
                item = db.ShoppingCarts.Where(x => x.CartID == guid).FirstOrDefault();

                if (existCart.IsNotNull())
                {
                    // find the data in ShoppingCartItems based on ShoppingCartID then replace with existing ShoppingCartID
                    db.ShoppingCartItems
                       .Where(x => x.ShoppingCartID == item.ShoppingCartIID)
                       .ToList()
                       .ForEach(x => x.ShoppingCartID = existCart.ShoppingCartIID);

                    db.ShoppingCartVoucherMaps
                       .Where(x => x.ShoppingCartID == item.ShoppingCartIID)
                       .ToList()
                       .ForEach(x => x.ShoppingCartID = existCart.ShoppingCartIID);

                    db.SaveChanges();

                    // delete the GUID row from ShoppingCarts table, cannot be removed it should make it Invalid
                    item.CartStatusID = (int)ShoppingCartStatus.Invalid;
                }
                else
                {
                    item.CartID = customerId;
                }
                db.SaveChanges();

                if (existCart.IsNotNull())
                {
                    // remove multiple Item with same SKU 
                    var listSKU = (from sci in db.ShoppingCartItems
                                   where sci.ShoppingCartID == existCart.ShoppingCartIID
                                   group sci by sci.ProductSKUMapID into grp
                                   where grp.Count() > 1
                                   select grp.Key).ToList();

                    foreach (var SKU in listSKU)
                    {
                        var cartItem = (from sci in db.ShoppingCartItems
                                        where sci.ShoppingCartID == existCart.ShoppingCartIID && sci.ProductSKUMapID == SKU
                                        orderby sci.ShoppingCartItemID ascending
                                        select sci).ToList();
                        var quantity = cartItem.Sum(x => x.Quantity);
                        var isFirst = true;
                        foreach (var sku in cartItem)
                        {
                            if (isFirst)
                            {
                                sku.Quantity = quantity;
                                isFirst = false;
                            }
                            else
                            {
                                db.ShoppingCartItems.Remove(sku);
                            }
                        }
                        item.CartStatusID = (int)ShoppingCartStatus.Invalid;
                    }
                    db.SaveChanges();
                }
                result = true;
            }
            return result;
        }

        public int ProductCount(string customerID, int? siteID, ShoppingCartStatus? shoppingCartStatusID = null)
        {
            var productCount = 0;
            int? cartStatusID = null;
            if (shoppingCartStatusID.IsNotNull())
                cartStatusID = (int)shoppingCartStatusID;

            using (var db = new dbEduegateERPContext())
            {
                productCount = (from sc in db.ShoppingCarts
                                join sci in db.ShoppingCartItems on sc.ShoppingCartIID equals sci.ShoppingCartID
                                where sc.CartID == customerID && sc.CartStatusID == cartStatusID && sc.SiteID == siteID
                                select sci).Count();
            }

            return productCount;
        }


        public bool UpdateCartStatus(long shoppingCartID, ShoppingCartStatus statusID, int? siteID, string paymentMethod = null, ShoppingCartStatus existingStatus = ShoppingCartStatus.None)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                ShoppingCart cart;

                if (existingStatus == ShoppingCartStatus.None)
                {
                    cart = dbContext.ShoppingCarts.Where(x => x.ShoppingCartIID == shoppingCartID && x.SiteID == siteID).FirstOrDefault();
                }
                else
                {
                    cart = dbContext.ShoppingCarts.Where(x => x.ShoppingCartIID == shoppingCartID && x.CartStatusID == (int)existingStatus && x.SiteID == siteID).FirstOrDefault();
                    if (cart != null && statusID == ShoppingCartStatus.PaymentInitiated)
                    {
                        var customerID = cart.CartID;
                        dbContext.ShoppingCarts.Where(x => x.CartID == customerID && x.CartStatusID == (int)statusID && x.ShoppingCartIID != shoppingCartID && x.SiteID == siteID).ToList().
                            ForEach(x =>
                            {
                                x.CartStatusID = (int)ShoppingCartStatus.Duplicated;
                                x.UpdatedDate = DateTime.Now;
                            });
                        dbContext.SaveChanges();
                    }
                }

                if (cart.IsNotNull())
                {
                    cart.CartStatusID = (int)statusID;
                    cart.PaymentMethod = paymentMethod.IsNullOrEmpty() ? cart.PaymentMethod : paymentMethod;
                    cart.UpdatedDate = DateTime.Now;
                    dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateCartCustomerID(long shoppingCartID, long customerID)
        {
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    ShoppingCart cart = dbContext.ShoppingCarts.Where(x => x.ShoppingCartIID == shoppingCartID).FirstOrDefault();
                    if (cart != null)
                    {
                        cart.CartID = customerID.ToString();
                        dbContext.SaveChanges();
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCartPaymentGateWay(long cartID, ShoppingCartStatus cartStatuID, Int16 PaymentGateWayID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool result = false;
                ShoppingCart cart = dbContext.ShoppingCarts.Where(x => x.ShoppingCartIID == cartID && x.CartStatusID == (int)cartStatuID).FirstOrDefault();
                if (cart.IsNotNull())
                {
                    cart.PaymentGateWayID = PaymentGateWayID;
                    cart.UpdatedDate = DateTime.Now;
                    dbContext.SaveChanges();
                    result = true;
                }
                return result;
            }
        }

        public ShoppingCart UpdateCartDescription(ShoppingCart CartDetails)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                bool result = false;
                ShoppingCart cart = dbContext.ShoppingCarts.Where(x => x.ShoppingCartIID == CartDetails.ShoppingCartIID && x.CartStatusID == (int)CartDetails.CartStatusID).FirstOrDefault();
                if (cart.IsNotNull())
                {
                    cart.Description = CartDetails.Description;
                    cart.UpdatedDate = DateTime.Now;
                    dbContext.SaveChanges();
                    result = true;
                }
                if (result)
                    return dbContext.ShoppingCarts.Where(x => x.ShoppingCartIID == CartDetails.ShoppingCartIID && x.CartStatusID == (int)CartDetails.CartStatusID).FirstOrDefault();
                else return null;
            }
        }


        public bool UpdateCart(ShoppingCart cart)
        {
            var result = false;
            try
            {
                using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
                {
                    dbContext.ShoppingCarts.Attach(cart);
                    var entry = dbContext.Entry(cart);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    dbContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCartRepository>.Fatal(exception.Message, exception);
            }
            return result;
        }

        public bool RemoveItem(long SKUID, string customerId, int? siteID)
        {
            var result = false;
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    var cartStatusID = (int)ShoppingCartStatus.InProcess;

                    // get list of cart items based on customer
                    var cartItems = (from sc in db.ShoppingCarts
                                     join sci in db.ShoppingCartItems on
                                     sc.ShoppingCartIID equals sci.ShoppingCartID

                                     where sc.CartID == customerId
                                     && sc.CartStatusID == cartStatusID && sc.SiteID == siteID
                                     select sci).ToList();

                    // filter data based on SKU 
                    var cartItem = cartItems.Where(x => x.ProductSKUMapID == SKUID).FirstOrDefault();


                    if (cartItem.IsNull())
                        return false;



                    if (cartItems.Count == 1)
                    {

                        var cart = (from sc in db.ShoppingCarts
                                    where sc.ShoppingCartIID == cartItem.ShoppingCartID && sc.SiteID == siteID
                                    select sc).FirstOrDefault();
                        if (cart.IsNull())
                            return false;

                        var voucherMap = (from vm in db.ShoppingCartVoucherMaps
                                          where vm.ShoppingCartID == cartItem.ShoppingCartID
                                          select vm).ToList();

                        var weekDays = db.ShoppingCartWeekDayMaps.Where(a=> a.ShoppingCartID == cartItem.ShoppingCartID).ToList();

                        foreach (var weekDay in weekDays)
                        {
                            db.ShoppingCartWeekDayMaps.Remove(weekDay);
                        }

                        if (voucherMap.IsNotNull())
                        {
                            foreach (var item in voucherMap)
                            {
                                // remove CartVoucherMaps
                                db.ShoppingCartVoucherMaps.Remove(item);
                            }
                        }
                        // remove cart item
                        db.ShoppingCartItems.Remove(cartItem);
                        // remove cart
                        db.ShoppingCarts.Remove(cart);
                    }
                    else
                    {
                        // remove cart item
                        db.ShoppingCartItems.Remove(cartItem);
                    }

                    db.SaveChanges();

                    result = true;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCartRepository>.Fatal(exception.Message, exception);
            }

            return result;
        }
        #endregion



        #region Voucher Methods
        public ShoppingCartVoucherMap ApplyVoucher(ShoppingCartVoucherMap voucherMap)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.ShoppingCartVoucherMaps.Add(voucherMap);
                db.SaveChanges();
            }
            return voucherMap;
        }

        public Voucher GetVoucher(string voucherNumber, long? customerID = null, int? companyID = null)
        {
            var voucher = new Voucher();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // get voucher based on voucher number
                voucher = db.Vouchers.Where(x => x.VoucherNo == voucherNumber && (x.StatusID == (int)Eduegate.Framework.Enums.VoucherStatus.Active || x.StatusID == (int)Eduegate.Framework.Enums.VoucherStatus.Redeemed) && x.CompanyID == companyID).FirstOrDefault();

                //if (voucher != null && voucher.CurrentBalance != null && voucher.CurrentBalance > 0)
                //{
                //    // check based on voucher type
                //    switch ((VoucherTypes)voucher.VoucherTypeID)
                //    {
                //        case VoucherTypes.Customer:
                //            if (voucher.CustomerID == customerID)
                //                return voucher;

                //            else if (voucher.CustomerID != customerID && voucher.ExpiryDate < DateTime.Now)
                //                voucher = null;
                //            break;
                //        case VoucherTypes.Public:
                //            if (voucher.ExpiryDate < DateTime.Now)
                //                voucher = null;
                //            break;
                //    }
                //}

            }
            return voucher;
        }

        public Voucher GetVoucher(long voucherID)
        {
            var voucher = new Voucher();
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // get voucher based on voucher number
                voucher = db.Vouchers.Where(x => x.VoucherIID == voucherID && (x.StatusID == (int)Eduegate.Framework.Enums.VoucherStatus.Active || x.StatusID == (int)Eduegate.Framework.Enums.VoucherStatus.Redeemed)).FirstOrDefault();
            }
            return voucher;
        }


        public ShoppingCartVoucherMap GetVoucherMap(long shoppingCartID)
        {
            var voucherMap = new ShoppingCartVoucherMap();

            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                voucherMap = (from vm in db.ShoppingCartVoucherMaps
                              where vm.ShoppingCartID == shoppingCartID
                              select vm).FirstOrDefault();
            }

            if (voucherMap != null && voucherMap.Amount != null && voucherMap.Amount > 0)
            {
                voucherMap.Amount = Convert.ToDecimal(voucherMap.Amount);
            }
            return voucherMap;
        }
        #endregion

        public DataTable CartDeliveryList(Int64 ShippingAddressID, Int64 ShoppingCartID, bool IsLocalDeliveryType, int? SiteID = 2)
        {
            DataTable cartDeliveryList = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("Inventory.spcProductDeliveryTypes", conn))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ShippingAddressID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ShippingAddressID"].Value = ShippingAddressID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ShoppingCartID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ShoppingCartID"].Value = ShoppingCartID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@SiteID", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@SiteID"].Value = SiteID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@IsLocalDeliveryType", SqlDbType.Bit));
                    adapter.SelectCommand.Parameters["@IsLocalDeliveryType"].Value = IsLocalDeliveryType;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    cartDeliveryList = dt.Tables[0];
                }

            }
            catch (Exception ex)
            {
            }

            return cartDeliveryList;
        }


        public DataTable CartDeliveryCost(Int64 ShippingAddressID, Int64 ShoppingCartID, Int32 CompanyID, bool IsLocal)
        {
            DataTable cartDeliveryList = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcCartDeliveryCharge]", conn))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ShippingAddressID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ShippingAddressID"].Value = ShippingAddressID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ShoppingCartID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ShoppingCartID"].Value = ShoppingCartID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@CompanyID"].Value = CompanyID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@IsLocal", SqlDbType.Bit));
                    adapter.SelectCommand.Parameters["@IsLocal"].Value = IsLocal;


                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    cartDeliveryList = dt.Tables[0];
                }

            }
            catch (Exception ex)
            {
            }

            return cartDeliveryList;
        }


        public DataTable AddToCartVerification(Int64 ProductSkuMapID, Int64 customerID, Int32 AddedCartQuantity, string CartID, Int32 CurrentInventoryBranchQty)
        {
            DataTable AddToCartStatus = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("inventory.spcAddToCartVerification", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ProductSkuMapID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ProductSkuMapID"].Value = ProductSkuMapID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@CustomerID"].Value = customerID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@AddedCartQuantity", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@AddedCartQuantity"].Value = AddedCartQuantity;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CartID", SqlDbType.NVarChar, 200));
                    adapter.SelectCommand.Parameters["@CartID"].Value = CartID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CurrentInventoryBranchQty", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@CurrentInventoryBranchQty"].Value = CurrentInventoryBranchQty;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    AddToCartStatus = dt.Tables[0];
                }

            }
            catch (Exception ex)
            {
            }

            return AddToCartStatus;
        }


        public DataTable CheckOutCartQuantityVerification(Int64 ProductSkuMapID, Int64 CustomerID, Int32 AddedCartQuantity, Int32 CurrentInventoryBranchQty)
        {
            DataTable CartQty = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcCartCheckOutQuantityVerificaton]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ProductSkuMapID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ProductSkuMapID"].Value = ProductSkuMapID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@CustomerID"].Value = CustomerID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@AddedCartQuantity", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@AddedCartQuantity"].Value = AddedCartQuantity;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CurrentInventoryBranchQty", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@CurrentInventoryBranchQty"].Value = CurrentInventoryBranchQty;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    CartQty = dt.Tables[0];
                }

            }
            catch (Exception ex)
            {
            }

            return CartQty;
        }


        public bool IsSerialNo(long productSkuMapID, long productID)
        {
            bool IsDigital = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var SkuInventoryConfigMaps = (from sc in db.ProductInventorySKUConfigMaps
                                              join cng in db.ProductInventoryConfigs on sc.ProductInventoryConfigID equals cng.ProductInventoryConfigIID
                                              where sc.ProductSKUMapID == productSkuMapID
                                              select cng).FirstOrDefault();
                if (SkuInventoryConfigMaps == null)
                {

                    var ProductInventoryConfigMaps = (from sc in db.ProductInventoryProductConfigMaps
                                                      join cng in db.ProductInventoryConfigs on sc.ProductInventoryConfigID equals cng.ProductInventoryConfigIID
                                                      where sc.ProductID == productID
                                                      select cng).FirstOrDefault();

                    if (ProductInventoryConfigMaps != null)
                    {
                        if (ProductInventoryConfigMaps.IsSerialNumber == true)
                        {
                            IsDigital = true;
                        }
                        else
                        {
                            IsDigital = false;
                        }
                    }
                    else
                    {
                        IsDigital = false;
                    }

                }
                else
                {
                    if (SkuInventoryConfigMaps.IsSerialNumber == true)
                    {
                        IsDigital = true;
                    }
                    else
                    {
                        IsDigital = false;
                    }
                }

            }
            return IsDigital;
        }

        public DataTable ShoppingCartItems(Int64 shoppingCartID, Int64 shoppingCartStatusID, int CultureID, CallContext callContext)
        {
            DataTable CartItemList = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcShoppingCartItems]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@shoppingCartID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@shoppingCartID"].Value = shoppingCartID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@shoppingCartStatusID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@shoppingCartStatusID"].Value = shoppingCartStatusID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CultureID", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@CultureID"].Value = CultureID;

                    if (callContext.IsNotNull() && callContext.CompanyID.IsNotNull() && callContext.CurrencyCode.IsNotNullOrEmpty())
                    {
                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int));
                        adapter.SelectCommand.Parameters["@CompanyID"].Value = callContext.CompanyID;

                        adapter.SelectCommand.Parameters.Add(new SqlParameter("@CurrencyCode", SqlDbType.NVarChar));
                        adapter.SelectCommand.Parameters["@CurrencyCode"].Value = callContext.CurrencyCode;
                    }

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    CartItemList = dt.Tables[0];
                }

            }
            catch (Exception ex)
            {
                CartItemList = null;
            }

            return CartItemList;
        }

        public bool ShoppingCartItemWeightUpdate(Int64 shoppingCartID, Int64 SKUID, Int32 CompanyID)
        {
            bool isSuccess = false;
            DataTable CartItemWeight = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationExtensions.GetConnectionString("dbEduegateERPContext").ToString()))
                using (SqlCommand cmd = new SqlCommand("[inventory].[spcShoppingCartNertweightUpdate]", conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@shoppingCartID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@shoppingCartID"].Value = shoppingCartID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ProductSKUMapID", SqlDbType.BigInt));
                    adapter.SelectCommand.Parameters["@ProductSKUMapID"].Value = SKUID;

                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@CompanyID", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@CompanyID"].Value = CompanyID;

                    DataSet dt = new DataSet();
                    adapter.Fill(dt);
                    CartItemWeight = dt.Tables[0];
                    if (CartItemWeight.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(CartItemWeight.Rows[0]["Status"]) == true) { isSuccess = true; }
                    }
                }

            }
            catch (Exception)
            {
                CartItemWeight = null;
            }

            return isSuccess;
        }

        public TransactionHead BlockInventoryForShoppingCartItems(TransactionHead head, long cartIID, bool blockStatus)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.TransactionHeads.Add(head);

                if (head.HeadIID > 0)
                {
                    db.Entry(head).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
                //update shopping cart blocking stat
                var cart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == cartIID).FirstOrDefault();
                cart.IsInventoryBlocked = blockStatus;
                cart.InventoryBlockedDateTime = blockStatus ? DateTime.Now : (DateTime?)null;
                cart.BlockedHeadID = head.HeadIID;
                db.SaveChanges();
                return head;
            }
        }

        public CartPaymentGateWayDetails GetCartStatusWithPaymentGateWayTrackKey(long ShoppingCartID, int PaymentGateWayID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                CartPaymentGateWayDetails cartPaymentDetails = new CartPaymentGateWayDetails(); ;
                var cart = GetCartDetailbyIID(ShoppingCartID);
                switch (PaymentGateWayID)
                {
                    case 10:
                        var paymentDetails = (from payment in db.PaymentDetailsTheForts where payment.CartID == ShoppingCartID select payment).FirstOrDefault();
                        if (cart.IsNotNull() && paymentDetails.IsNotNull())
                        {
                            cartPaymentDetails.PaymentGateWayID = PaymentGateWayID;
                            cartPaymentDetails.CartIID = cart.ShoppingCartIID;
                            cartPaymentDetails.CartStatusID = cart.CartStatusID.HasValue ? cart.CartStatusID.Value : 0;
                            cartPaymentDetails.TrackKey = paymentDetails.TrackKey;
                        }
                        break;
                    default:
                        break;
                }
                return cartPaymentDetails;
            }
        }

        public Site GetSiteByCompany(int? companyID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.Sites.Where(x => x.CompanyID == companyID).FirstOrDefault();
            }
        }

        public bool IsInternationalCart(long shippingAddressID, int? siteID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (from m in db.SiteCountryMaps
                        join c in db.Contacts on m.CountryID equals c.CountryID
                        where c.ContactIID == shippingAddressID && m.SiteID == siteID && m.IsLocal == false
                        select m).Any();
            }
        }

        public bool IsInternationalCartByCountryID(int countryID, int siteID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.SiteCountryMaps.Where(x => x.CountryID == countryID && x.SiteID == siteID && x.IsLocal == true).Any();
            }
        }

        public ProductDeliveryTypeMap GetDeliveryDays(long productSKUMapID, long companyID, int deliveryTypeID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.ProductDeliveryTypeMaps.Where(x => x.ProductSKUMapID == productSKUMapID && x.CompanyID == companyID && x.DeliveryTypeID == deliveryTypeID).FirstOrDefault();
            }
        }

        public DeliveryTypeTimeSlotMap GetDeliveryTimeSlot(int deliveryTypeID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                TimeSpan ts = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                return db.DeliveryTypeTimeSlotMaps.Where(x => x.DeliveryTypeID == deliveryTypeID && x.TimeFrom <= ts && x.TimeTo >= ts)
                    .Include("DeliveryTypeTimeSlotMapsCultures").FirstOrDefault();

            }
        }

        public void UpdateCartIsInternational(long shoppingCartIID, bool status)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                db.ShoppingCarts.Where(x => x.ShoppingCartIID == shoppingCartIID).ToList().ForEach(x => x.IsInternational = status);
                db.SaveChanges();
            }
        }

        public List<ShoppingCartItem> GetCartDetailWithItems(string customerID, int shoppingCartStatusID, int? siteID)
        {
            using (var db = new dbEduegateERPContext())
            {
                return db.ShoppingCartItems.Where(sc =>
                             sc.ShoppingCart.CartID == customerID
                             && sc.ShoppingCart.CartStatusID == shoppingCartStatusID
                             && sc.ShoppingCart.SiteID == siteID)
                    .ToList();
            }
        }

        public decimal GetMinimumCapForAnArea(long shippingAddressId)
        {
            using (var db = new dbEduegateERPContext())
            {
                var contact = db.Contacts.FirstOrDefault(x => x.ContactIID == shippingAddressId);
                var area = contact == null ? null : db.Areas.FirstOrDefault(x => x.AreaID == contact.AreaID);

                return contact == null || area == null ||
                    !area.MinimumCapAmount.HasValue ? 0 : area.MinimumCapAmount.Value;
            }
        }

        public bool RemoveAllItem(string customerId, int? siteID)
        {
            var result = false;
            try
            {
                using (var db = new dbEduegateERPContext())
                {
                    var cartStatusID = (int)ShoppingCartStatus.InProcess;

                    // get list of cart items based on customer
                    var cartItems = (from sc in db.ShoppingCarts
                                     join sci in db.ShoppingCartItems on
                                     sc.ShoppingCartIID equals sci.ShoppingCartID

                                     where sc.CartID == customerId
                                     && sc.CartStatusID == cartStatusID && sc.SiteID == siteID
                                     select sci).ToList();


                    var cartItem = cartItems.FirstOrDefault();
                    // remove cart item
                    db.ShoppingCartItems.RemoveRange(cartItems);

                    if (cartItem != null)
                    {
                        var cart = (from sc in db.ShoppingCarts
                                    where sc.ShoppingCartIID == cartItem.ShoppingCartID && sc.SiteID == siteID
                                    select sc).FirstOrDefault();

                        var voucherMap = (from vm in db.ShoppingCartVoucherMaps
                                          where vm.ShoppingCartID == cartItem.ShoppingCartID
                                          select vm).ToList();


                        if (voucherMap.IsNotNull())
                        {
                            foreach (var item in voucherMap)
                            {
                                // remove CartVoucherMaps
                                db.ShoppingCartVoucherMaps.Remove(item);
                            }
                        }

                        if (cart != null)
                        {
                            db.ShoppingCarts.Remove(cart);
                        }
                    }

                    db.SaveChanges();

                    result = true;
                }
            }
            catch (Exception exception)
            {
                Eduegate.Logger.LogHelper<ShoppingCartRepository>.Fatal(exception.Message, exception);
            }

            return result;
        }

        public long GetCustomerID(string loginEmailID, string mobileNumber = "", string userReferenceID = "", long? loginID = null)
        {
            using (var _dbContext = new dbEduegateERPContext())
            {
                Customer customer;

                if (loginID.HasValue)
                {
                    customer = _dbContext.Customers.FirstOrDefault(x => x.LoginID == loginID);
                }
                else
                {
                    customer = _dbContext.Customers.FirstOrDefault(x => x.Telephone == mobileNumber);
                }

                return customer != null ? customer.CustomerIID : 0;
            }
        }

        public bool UpdateCartAddress(long cartIID, long? shippingAddress, long? billingAddress)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var cart = dbContext.ShoppingCarts.FirstOrDefault(x => x.ShoppingCartIID == cartIID);
                cart.ShippingAddressID = shippingAddress;
                cart.BillingAddressID = billingAddress;
                dbContext.SaveChanges();
                return true;
            }
        }

        public List<ShoppingCart> GetPaymentPendingOrders(string customerId, int periodInDays)
        {
            using (var _dbContext = new dbEduegateERPContext())
            {
                return _dbContext.ShoppingCarts
                 .Include(x => x.ShoppingCartItems)
                 .Where(x => x.CartID == customerId && x.CartStatusID == (int)ShoppingCartStatus.PaymentInitiated
                    && x.UpdatedDate >= DateTime.Now.AddDays(-1 * periodInDays)).ToList();
            }
        }

        public string SaveShoppingCartDetails(CheckoutPaymentDTO checkoutPaymentDTO, CallContext callContext, long? customerIID = null)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var shoppingCartID = string.IsNullOrEmpty(checkoutPaymentDTO.ShoppingCartID) ? 0 : long.Parse(checkoutPaymentDTO.ShoppingCartID);

                var shoppingCartDetails = dbContext.ShoppingCarts.FirstOrDefault(x => x.ShoppingCartIID == shoppingCartID);

                if (shoppingCartDetails != null)
                {
                    shoppingCartDetails.StudentID = checkoutPaymentDTO.SelectedStudentID;
                    shoppingCartDetails.SchoolID = checkoutPaymentDTO.SelectedStudentSchoolID;
                    shoppingCartDetails.AcademicYearID = checkoutPaymentDTO.SelectedStudentAcademicYearID;

                    dbContext.Entry(shoppingCartDetails).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();

                    return "Data Modified";
                }
                else
                {
                    return null;
                }

            }
        }

        public bool UpdateStudentDetails(long cartIID, long? studentID, byte? schoolID, int? academicYearID)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var cart = dbContext.ShoppingCarts.FirstOrDefault(x => x.ShoppingCartIID == cartIID);
                cart.ShippingAddressID = studentID;
                cart.SchoolID = schoolID;
                cart.AcademicYearID = academicYearID;

                dbContext.SaveChanges();
                return true;
            }
        }

        public string CheckCustomerExist(UserDTO userDTO, CallContext callContext, long? userIID = null)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = callContext.LoginID;

                var customerExist = dbContext.Customers.FirstOrDefault(x => x.LoginID == loginID);

                var parent = dbContext.Parents.FirstOrDefault(x => x.LoginID == loginID);

                var branchID = dbContext.Settings.FirstOrDefault(X => X.SettingCode == "ONLINEBRANCHID").SettingValue;

                var custmr = dbContext.Customers.FirstOrDefault(x => x.LoginID == loginID);

                //var custmrID = custmr.CustomerIID;

                var customer = new Customer();

                var customerSetting = new CustomerSetting();

                if (customerExist == null)
                {
                    customer.LoginID = loginID;
                    customer.FirstName = parent.FatherFirstName;
                    customer.MiddleName = parent.FatherMiddleName;
                    customer.LastName = parent.FatherLastName;
                    customer.Telephone = parent.GuardianPhone;
                    customer.CustomerCode = parent.ParentCode;
                    customer.CustomerEmail = parent.GaurdianEmail;
                    customer.DefaultBranchID = long.Parse(branchID);
                    customer.StatusID = 1;

                    dbContext.Entry(customer).State = System.Data.Entity.EntityState.Added;

                    dbContext.SaveChanges();

                    return "Data Added";
                }
                else
                {
                    return "Already Exist";
                }

            }
        }

        public string CheckCustomerSettingExist(UserDTO userDTO, CallContext callContext, long? userIID = null)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = callContext.LoginID;

                var custmr = dbContext.Customers.FirstOrDefault(x => x.LoginID == loginID);

                var custmrID = custmr.CustomerIID;

                var customerSettingExist = dbContext.CustomerSettings.FirstOrDefault(x => x.CustomerID == custmrID);

                var customerSetting = new CustomerSetting();

                if (customerSettingExist == null)
                {
                    customerSetting.CustomerID = custmrID;
                    //customerSetting.CurrentLoyaltyPoints = decimal.Parse("0.00");
                    //customerSetting.TotalLoyaltyPoints = decimal.Parse("0.00");
                    customerSetting.IsConfirmed = true;
                    customerSetting.IsVerified = true;
                    //customerSetting.IsBlocked = false;

                    dbContext.Entry(customerSetting).State = System.Data.Entity.EntityState.Added;

                    dbContext.SaveChanges();

                    return "Data Added";
                }
                else
                {
                    return "Already Exist";
                }

            }
        }

        public string saveDefaultStudent(long studentID, CallContext callContext, long? userIID = null)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = callContext.LoginID;

                var customerExist = dbContext.Customers.FirstOrDefault(x => x.LoginID == loginID);

                {
                    customerExist.DefaultStudentID = studentID;

                    dbContext.Entry(customerExist).State = System.Data.Entity.EntityState.Modified;

                    dbContext.SaveChanges();
                }

                return "Data Added";


            }
        }

        public CustomerDTO getDefaultStudent(CallContext callContext)
        {
            using (var dbContext = new dbEduegateERPContext())
            {
                var loginID = callContext.LoginID;

                var customerExist = dbContext.Customers.FirstOrDefault(x => x.LoginID == loginID);

                var student = dbContext.Students.FirstOrDefault(a => a.StudentIID == customerExist.DefaultStudentID);

                var customer = new CustomerDTO();

                return new CustomerDTO()
                {
                    CustomerIID = customerExist.CustomerIID,
                    DefaultStudentID = customerExist.DefaultStudentID,
                    StudentName = customerExist.DefaultStudentID.HasValue ? student.FirstName + " " + student.MiddleName + " " + student.LastName : null,
                    StudentProfile = student.StudentProfile,
                };

                //return customer;


            }
        }

        public string UpdateCartScheduleDate(long cartID, DateTime? scheduleDate)
        {
            using (var db = new dbEduegateERPContext())
            {
                var cart = db.ShoppingCarts
                    .Where(x => x.ShoppingCartIID == cartID)
                    .FirstOrDefault();
                cart.ScheduledDateTime = scheduleDate;
                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return "Data Updated";
        }


        public bool SaveSchedule(long cartID, DateTime? StartDate, DateTime? EndDate, short? SubscriptionTypeID, List<byte> DaysID , int? DeliveryDaysCount)
        {
            if (DeliveryDaysCount == 0 )
            {
                return false;
            }

            using (var db = new dbEduegateERPContext())
            {
                var cart = db.ShoppingCarts
                    .Where(x => x.ShoppingCartIID == cartID)
                    .FirstOrDefault();

                cart.StartDate = StartDate;
                cart.EndDate = EndDate;
                cart.SubscriptionTypeID = SubscriptionTypeID;
                cart.DeliveryDaysCount = DeliveryDaysCount.HasValue ? DeliveryDaysCount  : 1;
                db.Entry(cart).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();


                var weekDay = db.ShoppingCartWeekDayMaps.Where(x => x.ShoppingCartID == cartID).ToList();

                var shoppingCartWeekDays = new List<ShoppingCartWeekDayMap>();

                if (weekDay != null && weekDay.Count > 0)
                {
                    db.ShoppingCartWeekDayMaps.RemoveRange(weekDay);
                }

                foreach (var day in DaysID)
                {
                    shoppingCartWeekDays.Add(new ShoppingCartWeekDayMap()
                    {
                        ShoppingCartID = cartID,
                        WeekDayID = day,
                    });
                }

                db.ShoppingCartWeekDayMaps.AddRange(shoppingCartWeekDays);
                db.SaveChanges();

            }
            return true;
        }



        public List<DaysDTO> GetWeekDays()
        {

            using (var db = new dbEduegateERPContext())
            {
                var Days = db.Days.ToList();
                var DaysDTOs = new List<DaysDTO>();


                foreach (var Day in Days)
                {
                    DaysDTOs.Add(new DaysDTO()
                    {
                        DayID = Day.DayID,
                        DayName = Day.DayName
                    });

                }
                return DaysDTOs;
            }


        }

        //enum DaysOfWeek
        //{
        //    Sun = 0,
        //    Mon = 1,
        //    Tue = 2,
        //    Wed = 3,
        //    Thu = 4,
        //    Fri = 5,
        //    Sat = 6
        //};

        //int GetDayOfWeekAsInteger(string dayOfWeek)
        //{
        //    switch (dayOfWeek)
        //    {
        //        case "SUN":
        //            return (int)DaysOfWeek.Sun;
        //        case "MON":
        //            return (int)DaysOfWeek.Mon;
        //        case "TUE":
        //            return (int)DaysOfWeek.Tue;
        //        case "WED":
        //            return (int)DaysOfWeek.Wed;
        //        case "THU":
        //            return (int)DaysOfWeek.Thu;
        //        case "FRI":
        //            return (int)DaysOfWeek.Fri;
        //        case "SAT":
        //            return (int)DaysOfWeek.Sat;
        //        default:
        //            return -1;
        //    }
        //}
        public long GetAcademicCalenderByDateRange(DateTime startDate, DateTime endDate ,string daysID)

        {

            //var daysID = daysIDs.ToList();


            var academicCalenderEventList = new List<AcademicCalenderEventDateDTO>();
            using (var dbContext = new dbEduegateERPContext())
            {
                MutualRepository mutualRepository = new MutualRepository();

                var daysIDs = daysID.Split(',');





                //var days = new List<int>();

                //foreach (var dayID in daysIDs)
                //{
                //    days.Add(GetDayOfWeekAsInteger(dayID));

                //}


                var academicCalenderTypeID = mutualRepository.GetSettingData("ACADEMIC_CALENDAR_TYPE_ID").SettingValue;

                byte? calendarTypeID = byte.Parse(academicCalenderTypeID);

                var acadamicCalenderEvent = (from acevents in dbContext.AcademicYearCalendarEvents.Where(acevent =>
                                       acevent.AcadamicCalendar.AcademicCalendarStatusID == 1 && acevent.IsThisAHoliday == true &&
                ((DbFunctions.TruncateTime(acevent.StartDate).Value >= startDate && DbFunctions.TruncateTime(acevent.EndDate).Value <= endDate)))
                                             select acevents).ToList();

                for (DateTime adate = startDate.Date; adate.Date <= endDate.Date; adate = adate.AddDays(1))
                {

                    var holidaydet = acadamicCalenderEvent.Where(x => x.StartDate <= adate && x.EndDate >= adate).FirstOrDefault();

                    var dayOfWeek = (int)adate.DayOfWeek;

                    //if (days.Any(x => x == dayOfWeek) && holidaydet.IsNull())
                        if (daysIDs.Any(x => x == dayOfWeek.ToString()) && holidaydet.IsNull())
                        {

                        academicCalenderEventList.Add(new AcademicCalenderEventDateDTO()
                        {

                            EventTitle = holidaydet.IsNotNull() ? holidaydet.EventTitle : null,
                            Description = holidaydet.IsNotNull() ? holidaydet.Description : null,
                            StartDate = holidaydet.IsNotNull() ? holidaydet.StartDate : null,
                            EndDate = holidaydet.IsNotNull() ? holidaydet.EndDate : null,
                            IsThisAHoliday = holidaydet.IsNotNull(),
                            ActualDate = adate,
                            DayOfWeek = dayOfWeek,
                            Day = adate.Day,
                            Month = adate.Month,
                            Year = adate.Year
                        });
                    }

                }
            }
            long academicCalenderEventListCount = academicCalenderEventList.Count();

            return academicCalenderEventListCount;
        }

        public Category GetCartDetailsWithItems(string customerID, int shoppingCartStatusID, int? siteID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var items = (from sci in db.ShoppingCartItems
                             join pskm in db.ProductSKUMaps on sci.ProductSKUMapID equals pskm.ProductSKUMapIID
                             join pcm in db.ProductCategoryMaps on pskm.ProductID equals pcm.ProductID
                             join cat in db.Categories on pcm.CategoryID equals cat.CategoryIID
                             where sci.ShoppingCart.CartID == customerID
                             && sci.ShoppingCart.CartStatusID == shoppingCartStatusID
                             && sci.ShoppingCart.SiteID == siteID
                             orderby cat.CategoryIID
                             select cat)
                             .FirstOrDefault();

                return items;
            }
        }

        public Category GetAddedCartDetailsWithItems(long? skuID)
        {
            using (var db = new dbEduegateERPContext())
            {
                var items = (from pskm in db.ProductSKUMaps 
                             join pcm in db.ProductCategoryMaps on pskm.ProductID equals pcm.ProductID
                             join cat in db.Categories on pcm.CategoryID equals cat.CategoryIID
                             where pskm.ProductSKUMapIID == skuID
                             orderby cat.CategoryIID
                             select cat)
                             .FirstOrDefault();

                return items;
            }
        }

    }
}