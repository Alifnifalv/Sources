using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class InvetoryTransaction
    {
        public InvetoryTransaction()
        {
            this.InvetoryTransactions1 = new List<InvetoryTransaction>();
        }

        public long InventoryTransactionIID { get; set; }
        public Nullable<int> SerialNo { get; set; }
        public Nullable<int> DocumentTypeID { get; set; }
        public string TransactionNo { get; set; }
        public Nullable<System.DateTime> TransactionDate { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public Nullable<long> BatchID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public Nullable<long> UnitID { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<long> LinkDocumentID { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<long> HeadID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual ICollection<InvetoryTransaction> InvetoryTransactions1 { get; set; }
        public virtual InvetoryTransaction InvetoryTransaction1 { get; set; }
        public virtual InvetoryTransaction InvetoryTransactions11 { get; set; }
        public virtual InvetoryTransaction InvetoryTransaction2 { get; set; }
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
