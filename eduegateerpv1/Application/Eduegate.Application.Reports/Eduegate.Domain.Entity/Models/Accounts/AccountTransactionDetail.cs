using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class AccountTransactionDetail
    {
        public long AccountTransactionDetailIID { get; set; }
        public Nullable<long> AccountTransactionHeadID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal? ReferenceQuantity { get; set; }
        public decimal? ReferenceRate { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> ReturnAmount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }

        public Nullable<long> DepartmentID { get; set; }
        public Nullable<int> CostCenterID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<System.DateTime> PaymentDueDate { get; set; }
        public Nullable<long> ProductSKUId { get; set; }
        public Nullable<decimal> AvailableQuantity { get; set; }
        public Nullable<decimal> CurrentAvgCost { get; set; }
        public Nullable<decimal> NewAvgCost { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> AccountType { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<decimal> UnPaidAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public string JobMissionNumber { get; set; }
        public Nullable<int> TaxTemplateID { get; set; }
        public Nullable<long> ReferenceReceiptID { get; set; }
        public Nullable<long> ReferencePaymentID { get; set; }
        public Nullable<decimal> TaxPercentage { get; set; }
        public virtual Account Account { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
        public virtual AccountTransactionHead AccountTransactionHead { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual Department Department { get; set; }
        public virtual Currency Currency { get; set; }
        public string ExternalReference1 { get; set; }
        public string ExternalReference2 { get; set; }
        public string ExternalReference3 { get; set; }
        public int? BudgetID { get; set; }
        public long? SubLedgerID { get; set; }
        public virtual Accounts_SubLedger Accounts_SubLedger { get; set; }
        public virtual Budget Budget { get; set; }
    }
}
