using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            this.ShoppingCartItems = new List<ShoppingCartItem>();
            this.ShoppingCartVoucherMaps = new List<ShoppingCartVoucherMap>();
            //this.TransactionHeadShoppingCartMaps = new List<TransactionHeadShoppingCartMap>();
        }

        public long ShoppingCartIID { get; set; }
        public string CartID { get; set; }
        public Nullable<int> CartStatusID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<long> BillingAddressID { get; set; }
        public Nullable<long> ShippingAddressID { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<bool> IsInventoryBlocked { get; set; }
        public Nullable<DateTime> InventoryBlockedDateTime { get; set; }
        public Nullable<long> BlockedHeadID { get; set; }
        public string Description { get; set; }
        public Nullable<Int16> PaymentGateWayID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> SiteID { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<bool> IsInternational { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
        public virtual ICollection<ShoppingCartVoucherMap> ShoppingCartVoucherMaps { get; set; }
        public virtual ICollection<TransactionHeadShoppingCartMap> TransactionHeadShoppingCartMaps { get; set; }
    }
}
