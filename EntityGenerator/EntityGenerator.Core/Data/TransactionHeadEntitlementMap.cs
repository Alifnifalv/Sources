using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHeadEntitlementMap", Schema = "inventory")]
    [Index("TransactionHeadID", Name = "IDX_TransactionHeadEntitlementMap_TransactionHeadID_")]
    public partial class TransactionHeadEntitlementMap
    {
        [Key]
        public long TransactionHeadEntitlementMapIID { get; set; }
        public long TransactionHeadID { get; set; }
        public byte EntitlementID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Amount { get; set; }
        [StringLength(25)]
        public string ReferenceNo { get; set; }
        public long? PaymentTrackID { get; set; }
        [StringLength(50)]
        public string PaymentTransactionNumber { get; set; }

        [ForeignKey("EntitlementID")]
        [InverseProperty("TransactionHeadEntitlementMaps")]
        public virtual EntityTypeEntitlement Entitlement { get; set; }
        [ForeignKey("TransactionHeadID")]
        [InverseProperty("TransactionHeadEntitlementMaps")]
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
