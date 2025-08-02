using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PurchaseInvoiceSearchView
    {
        public long HeadIID { get; set; }
        public string TransactionNo { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<System.DateTime> DeliveryDate { get; set; }
        public string Status { get; set; }
        public string RowCategory { get; set; }
        public string EntitlementName { get; set; }
        public string ShoppingCartIID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string PurchaseOrder { get; set; }
        public string Reference { get; set; }
        public string ProductOwner { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public string PartNumber { get; set; }
        public string DisplayCode { get; set; }
        public int CommentCounts { get; set; }
    }
}
