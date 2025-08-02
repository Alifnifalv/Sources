using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderItem
    {
        [Key]
        public long OrderItemID { get; set; }
        public long RefOrderID { get; set; }
        public int RefOrderProductID { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscountPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ItemAmount { get; set; }
        public bool ItemCanceled { get; set; }
        public string CancelledBy { get; set; }
        public Nullable<byte> RefOrderStatusID { get; set; }
        public Nullable<int> ReturnApproveQty { get; set; }
        public decimal ProductMultiPrice { get; set; }
        public Nullable<byte> DeliveryDays { get; set; }
        public Nullable<bool> ExpressDelivery { get; set; }
        public Nullable<bool> PickupShowroom { get; set; }
        public Nullable<bool> SendbyEmail { get; set; }
        public Nullable<bool> IsNextDay { get; set; }
        public Nullable<bool> IsDigital { get; set; }
        public string DigitalDelivered { get; set; }
        public Nullable<bool> FreeGift { get; set; }
        public string DeliveryRemarks { get; set; }
        public string DeliveryRemarksAr { get; set; }
        public virtual OrderMaster OrderMaster { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
        public virtual ProductMaster ProductMaster1 { get; set; }
    }
}
