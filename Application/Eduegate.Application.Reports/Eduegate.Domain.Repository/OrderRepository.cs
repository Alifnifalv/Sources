using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Eduegate.Domain.Entity.Models;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Services.Contracts.Checkout;

namespace Eduegate.Domain.Repository
{
    public class OrderRepository
    {
        public bool SaveTransactionHeadCartMap(TransactionHeadShoppingCartMap transactionHeadCartMap)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                dbContext.TransactionHeadShoppingCartMaps.Add(transactionHeadCartMap);
                dbContext.SaveChanges();
                isSuccess = true;
            }
            return isSuccess;
        }

        public bool UpdateOrderIdPaymentDetailsKnet(long trackID, long transactionHeadId, long loginId)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var payments = dbContext.PaymentDetailsKnets.Where(x => x.TrackID == trackID && x.CustomerID == loginId).FirstOrDefault();

                if (payments != null)
                {
                    payments.OrderID = transactionHeadId;
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public bool UpdateOrderIdPaymentMasterVisa(long trackID, long transactionHeadId, long loginId, decimal cartAmount)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var cust = dbContext.Customers.Where(x => x.LoginID == loginId).FirstOrDefault();

                long customerID = cust.CustomerIID;

                var payments = dbContext.PaymentMasterVisas.Where(x => x.CustomerID == loginId && x.PaymentAmount == cartAmount).OrderByDescending((x => x.TrackIID)).FirstOrDefault();
                if (payments != null)
                {
                    payments.OrderID = transactionHeadId;
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public bool UpdateOrderIdPaymentPaypal(long trackID, long transactionHeadId, long loginId)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var payments = dbContext.PaymentDetailsPayPals.Where(x => x.TrackID == trackID && x.RefCustomerID == loginId).FirstOrDefault();

                if (payments != null)
                {
                    payments.OrderID = transactionHeadId;
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public bool UpdateOrderIdPaymentTheFort(long trackKey, long transactionHeadId, long loginId)
        {
            bool isSuccess = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                var payments = dbContext.PaymentDetailsTheForts.Where(x => x.TrackKey == trackKey && x.CustomerID == loginId).FirstOrDefault();

                if (payments != null)
                {
                    payments.OrderID = transactionHeadId;
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public bool SaveOrderAddress(List<OrderContactMap> contacts)
        {
            var exit = false;
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                foreach (var contact in contacts)
                {

                    var entry = dbContext.Entry(contact);
                    if (contact.OrderContactMapIID == default(long))
                    {
                        //insert

                        dbContext.OrderContactMaps.Add(contact);
                        //entry.State = EntityState.Added;
                    }
                    else
                    {
                        //update
                        dbContext.OrderContactMaps.Attach(contact);
                        entry.State = EntityState.Modified;
                    }
                }
                dbContext.SaveChanges();
                exit = true;
            }
            return exit;
        }

        public List<OrderContactMap> GetOrderContacts(long orderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.OrderContactMaps.Where(x => x.OrderID == orderID).ToList();
            }
        }

        public bool UpdateOrderTrackingStatus(OrderTracking track)
        {
            var exit = false;
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {

                var trans = db.TransactionHeads.Where(x => x.HeadIID == track.OrderID).SingleOrDefault();

                // Update Transaction table
                trans.TransactionStatusID = track.StatusID;
                trans.UpdatedBy = (int)track.CreatedBy;
                trans.UpdatedDate = DateTime.Now;

                // Insert in OrderTracking
                db.OrderTrackings.Add(new OrderTracking { OrderID = track.OrderID, StatusID = track.StatusID, StatusDate = track.StatusDate, CreatedBy = track.CreatedBy, CreatedDate = track.CreatedDate, Description = track.Description });

                // Save
                db.SaveChanges();
                exit = true;
            }
            return exit;
        }
        public DeliveryCharge GetDeliveryCharges(long contactID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var deliveryCharge = (from c in db.Contacts
                                      join d in db.DeliveryCharges on c.CountryID equals d.ToCountryID
                                      where c.ContactIID == contactID
                                      select d).ToList().FirstOrDefault();

                return deliveryCharge;
            }
        }

        public List<OrderTracking> GetOrderTrack(long orderID)
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.OrderTrackings.Where(x => x.OrderID == orderID).ToList();
            }
        }

        public List<TransactionStatus> GetTransactionList()
        {
            using (dbEduegateERPContext dbContext = new dbEduegateERPContext())
            {
                return dbContext.TransactionStatuses.OrderBy(a=> a.Description).ToList();
            }
        }

        public OrderContactMap SaveOrderContactMap(OrderContactMap entity)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                // 
                db.OrderContactMaps.Add(entity);
                if (entity.OrderContactMapIID > 0)
                {
                    // Update
                    db.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    // Insert
                    db.Entry(entity).State = EntityState.Added;
                }
                db.SaveChanges();
                return entity;
            }
        }

        public TransactionHead SaveWebsiteOrder(CheckoutPaymentDTO dto, string DocumentID, string transactionNo, long loginID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var shoppingcartID = long.Parse(dto.ShoppingCartID);
                var shoppingCart = db.ShoppingCarts.Where(a => a.ShoppingCartIID == shoppingcartID).FirstOrDefault();
                db.Entry(shoppingCart).Collection(a => a.ShoppingCartItems).Load();
                shoppingCart.CartStatusID = (int)Eduegate.Framework.Helper.Enums.ShoppingCartStatus.Paid;
                shoppingCart.PaymentMethod = dto.SelectedPaymentOption;
                shoppingCart.ShippingAddressID = long.Parse(dto.SelectedShippingAddress);
                if (string.IsNullOrEmpty(dto.SelectedBillingAddress))
                {
                    var contact = db.Contacts.Where(a => a.LoginID == loginID && a.IsBillingAddress == true).FirstOrDefault();
                    shoppingCart.BillingAddressID = contact.ContactIID;
                }
                else
                {
                    shoppingCart.BillingAddressID = long.Parse(dto.SelectedShippingAddress);
                }
                var transactionHead = ShoppingtoTransactionEntity(shoppingCart, DocumentID, dto, transactionNo);
                transactionHead.TransactionStatusID = (byte)Eduegate.Framework.Enums.TransactionStatus.New;
                db.TransactionHeads.Add(transactionHead);
                db.SaveChanges();

                var customerID = shoppingCart.CartID;
                foreach (var item in shoppingCart.ShoppingCartItems)
                {
                    db.TransactionDetails.Add(ShoppingtoTransactionDetail(transactionHead.HeadIID, item));
                    db.SaveChanges();

                }
                db.Entry(transactionHead).Collection(a => a.TransactionDetails).Load();

                db.TransactionHeadShoppingCartMaps.Add(new TransactionHeadShoppingCartMap() { TransactionHeadID = transactionHead.HeadIID, ShoppingCartID = shoppingcartID });
                //var transactionNo = 
                //db.TransactionHeads.Add
                db.SaveChanges();
                return transactionHead;
            }
        }

        public TransactionHead ShoppingtoTransactionEntity(ShoppingCart cart, string DocumentID, CheckoutPaymentDTO dto, string transactionNo)
        {
            return new TransactionHead()
            {
                BranchID = 1,
                DocumentTypeID = int.Parse(DocumentID),
                TransactionNo = transactionNo,
                CustomerID = long.Parse(cart.CartID),
                DeliveryMethodID = 1,
                EntitlementID = byte.Parse(dto.SelectedPaymentOption),
                TransactionDate = DateTime.Today,
                CurrencyID = dto.CurrencyID,
            };
        }

        public TransactionDetail ShoppingtoTransactionDetail(long headID, ShoppingCartItem item)
        {
            return null;
            //return new TransactionDetail()
            //{
            //    HeadID = headID,
            //    ProductID = GetProductIDfromSKU((long)item.ProductSKUMapID),
            //    ProductSKUMapID = item.ProductSKUMapID,
            //    Quantity = item.Quantity,
            //    UnitPrice = new ProductCatalogRepository().GetProductPrice((long)item.ProductSKUMapID),
            //    Amount = item.Quantity * new ProductCatalogRepository().GetProductPrice((long)item.ProductSKUMapID),
            //};
        }

        private long GetProductIDfromSKU(long skuID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return (long)db.ProductSKUMaps.Where(a => a.ProductSKUMapIID == skuID).Select(b => b.ProductID).FirstOrDefault();
            }
        }

        public TransactionHead GetTransactionDetails(long headID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var transactionHead = db.TransactionHeads.Where(a => a.HeadIID == headID).FirstOrDefault();
                db.Entry(transactionHead).Reference(a => a.Customer).Load();
                db.Entry(transactionHead).Reference(a => a.Supplier).Load();
                db.Entry(transactionHead).Reference(a => a.Currency).Load();
                db.Entry(transactionHead).Reference(a => a.DeliveryType).Load();
                return transactionHead;
            }
        }
        public DateTime? GetLastUsedVoucherOrder(long voucherID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var shoppingcartVoucherMap = db.ShoppingCartVoucherMaps.Where(a => a.VoucherID == voucherID).OrderByDescending(a => a.CreatedBy).FirstOrDefault();
                if (shoppingcartVoucherMap != null)
                {
                    var transheadShoppingCartMap = db.TransactionHeadShoppingCartMaps.Where(a => a.ShoppingCartID == shoppingcartVoucherMap.ShoppingCartID).FirstOrDefault();
                    if (transheadShoppingCartMap != null)
                    {
                        var transactionHead = db.TransactionHeads.Where(a => a.HeadIID == transheadShoppingCartMap.TransactionHeadID).FirstOrDefault();
                        if (transactionHead != null)
                        {
                            return transactionHead.TransactionDate;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public bool AddTransactionHeadEntitlementMap(long headID, Eduegate.Framework.Payment.EntitlementType entitlementTypeID, decimal amount)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    var transactionHeadEntitlementMap = new TransactionHeadEntitlementMap();
                    transactionHeadEntitlementMap.TransactionHeadID = headID;
                    transactionHeadEntitlementMap.EntitlementID = (byte)entitlementTypeID;
                    transactionHeadEntitlementMap.Amount = amount;
                    db.TransactionHeadEntitlementMaps.Add(transactionHeadEntitlementMap);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateCustomerLoyaltyPoints(decimal cartAmount, decimal loyaltyPointsValue, decimal categorizationPointsValue, long headID, long customerID)
        {
            try
            {
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    long loyaltyPoints = 0, categorizationPoints = 0;
                    try
                    {
                        loyaltyPoints = (long)(cartAmount / loyaltyPointsValue);
                    }
                    catch (Exception ex) { }
                    try
                    {
                        categorizationPoints = (long)(cartAmount / categorizationPointsValue);
                    }
                    catch (Exception ex) { }

                    var transactionHeadPointsMap = new TransactionHeadPointsMap();
                    transactionHeadPointsMap.TransactionHeadID = headID;
                    transactionHeadPointsMap.LoyaltyPoints = loyaltyPoints;
                    transactionHeadPointsMap.CategorizationPoints = categorizationPoints;

                    db.TransactionHeadPointsMaps.Add(transactionHeadPointsMap);
                    var customerEntity = db.CustomerSettings.Where(a => a.CustomerID == customerID).FirstOrDefault();
                    if (customerEntity != null)
                    {
                        customerEntity.CurrentLoyaltyPoints = customerEntity.CurrentLoyaltyPoints + loyaltyPoints;
                        customerEntity.TotalLoyaltyPoints = customerEntity.TotalLoyaltyPoints + categorizationPoints;
                    }

                    db.Entry(customerEntity).State = EntityState.Modified;
                    db.SaveChanges();
                    SetCustomerGroup(customerID);

                    return true;
                }
            }
            catch (Exception ex) { return false; }

        }

        public bool SetCustomerGroup(long customerID)
        {
            try
            {
                var customerGroup = new CustomerGroup();
                using (dbEduegateERPContext db = new dbEduegateERPContext())
                {
                    var totalLoyaltyPoints = db.CustomerSettings.Where(a => a.CustomerID == customerID).FirstOrDefault().TotalLoyaltyPoints;
                    if (totalLoyaltyPoints > 0)
                    {
                        //customerGroup = db.CustomerGroups.TakeWhile(a => a.PointLimit <= totalLoyaltyPoints).OrderByDescending(a => a.PointLimit).First();
                        customerGroup = db.CustomerGroups.Where(a => (totalLoyaltyPoints - a.PointLimit) >= 0).OrderByDescending(a => a.PointLimit).First();
                    }
                    var customerEntity = db.Customers.Where(a => a.CustomerIID == customerID).FirstOrDefault();
                    if (customerGroup != null)
                    {
                        if (customerGroup.CustomerGroupIID != 0)
                        {
                            customerEntity.GroupID = customerGroup.CustomerGroupIID;
                        }
                        else
                        {
                            customerEntity.GroupID = null;
                        }
                    }
                    else
                    {
                        customerEntity.GroupID = null;
                    }
                    db.Entry(customerEntity).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public long GetOrderIDFromOrderNo(string transactionNo)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var headID = db.TransactionHeads.Where(a => a.TransactionNo == transactionNo).FirstOrDefault();
                if (headID != null)
                {
                    return headID.HeadIID;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Eduegate.Framework.Helper.Enums.DeliveryTypes GetDeliveryType(long headID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                var deliveryType = db.TransactionHeads.Where(a => a.HeadIID == headID).Select(a => a.DeliveryTypeID).FirstOrDefault();
                return deliveryType.HasValue ? (Eduegate.Framework.Helper.Enums.DeliveryTypes)(deliveryType) : Eduegate.Framework.Helper.Enums.DeliveryTypes.None;
            }
        }

        public bool isOrderOfUser(long headID, long customerID)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                return db.TransactionHeads.Where(a => a.CustomerID == customerID && a.HeadIID == headID).Any();
            }
        }

        public bool InsertOrderRoleTracking(long transactionHeadID, string devicePlatform, string deviceVersion)
        {
            using (dbEduegateERPContext db = new dbEduegateERPContext())
            {
                try
                {
                    var orderRoleTracking = new OrderRoleTracking();
                    orderRoleTracking.DevicePlatform = devicePlatform;
                    orderRoleTracking.DeviceVersion = deviceVersion;
                    orderRoleTracking.TransactionHeadID = transactionHeadID;
                    db.OrderRoleTrackings.Add(orderRoleTracking);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception) { return false; }
            }
        }
    }
}
