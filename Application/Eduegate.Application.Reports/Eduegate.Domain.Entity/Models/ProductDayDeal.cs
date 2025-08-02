using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDayDeal
    {
        public int DealID { get; set; }
        public int RefProductID { get; set; }
        public decimal DealPrice { get; set; }
        public int DealQty { get; set; }
        public bool DealPickUpShowroom { get; set; }
        public Nullable<int> DealMaxOrderQty { get; set; }
        public Nullable<int> DealMaxOrderQtyVerified { get; set; }
        public int DealMaxCustomerQty { get; set; }
        public int DealMaxCustomerQtyDuration { get; set; }
        public Nullable<System.DateTime> StartDateTime { get; set; }
        public System.DateTime EndDateTime { get; set; }
        public string Status { get; set; }
        public long RefUserID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool ContinueDeal { get; set; }
        public Nullable<System.DateTime> ContinueDealEndDateTime { get; set; }
        public decimal CurrentPrice { get; set; }
        public int DealPosition { get; set; }
    }
}
