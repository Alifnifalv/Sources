using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WalletTransactionDetail
    {
        public long WalletTransactionId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string CustomerWalletTranRef { get; set; }
        public short RefTransactionRelationId { get; set; }
        public long CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string AdditionalDetails { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<short> StatusId { get; set; }
        public Nullable<long> TrackId { get; set; }
        public string RefOrderId { get; set; }
        public string PaymentMethod { get; set; }
        public Nullable<System.DateTime> ModifiedDateTime { get; set; }
        public virtual CustomerMaster CustomerMaster { get; set; }
    }
}
