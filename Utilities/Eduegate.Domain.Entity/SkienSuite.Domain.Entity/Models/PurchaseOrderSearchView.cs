using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PurchaseOrderSearchView
    {
        public long HeadIID { get; set; }
        public string TransactionNo { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string RowCategory { get; set; }
        public string Status { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string PIStatus { get; set; }
        public string PartNumber { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string ShoppingCartIID { get; set; }
        public string EntitlementName { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Country { get; set; }
        public string ProductOwner { get; set; }
        public string JobStatus { get; set; }
        public string DisplayCode { get; set; }
        public int CommentCounts { get; set; }
        public string DeliveryTypeName { get; set; }
        public string ReceivingMethodName { get; set; }
    }
}
