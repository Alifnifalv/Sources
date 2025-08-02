using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransactionHeadAccountMaps", Schema = "inventory")]
    public partial class TransactionHeadAccountMap
    {
        [Key]
        public long TransactionHeadAccountMapIID { get; set; }
        public long? TransactionHeadID { get; set; }
        public long? AccountTransactionID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AccountTransactionID")]
        [InverseProperty("TransactionHeadAccountMaps")]
        public virtual AccountTransaction AccountTransaction { get; set; }
        [ForeignKey("TransactionHeadID")]
        [InverseProperty("TransactionHeadAccountMaps")]
        public virtual TransactionHead TransactionHead { get; set; }
    }
}
