namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("inventory.TransactionHeadAccountMaps")]
    public partial class TransactionHeadAccountMap
    {
        [Key]
        public long TransactionHeadAccountMapIID { get; set; }

        public long? TransactionHeadID { get; set; }

        public long? AccountTransactionID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual AccountTransaction AccountTransaction { get; set; }

        public virtual TransactionHead TransactionHead { get; set; }
    }
}
