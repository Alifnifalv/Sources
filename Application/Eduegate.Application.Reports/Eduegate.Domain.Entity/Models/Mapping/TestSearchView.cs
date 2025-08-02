using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class TestSearchView
    {
        public long WalletTransactionId { get; set; }
        public string CustomerWalletTranRef { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public short TransactionRelationId { get; set; }
        public int LanguageID { get; set; }
        public int Expr1 { get; set; }
    }
}
