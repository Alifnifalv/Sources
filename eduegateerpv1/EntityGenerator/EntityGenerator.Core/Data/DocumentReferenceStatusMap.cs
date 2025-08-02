using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DocumentReferenceStatusMap", Schema = "mutual")]
    public partial class DocumentReferenceStatusMap
    {
        public DocumentReferenceStatusMap()
        {
            AssetTransactionHeads = new HashSet<AssetTransactionHead>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [Key]
        public long DocumentReferenceStatusMapID { get; set; }
        public int? ReferenceTypeID { get; set; }
        public long DocumentStatusID { get; set; }

        [ForeignKey("DocumentStatusID")]
        [InverseProperty("DocumentReferenceStatusMaps")]
        public virtual DocumentStatus DocumentStatus { get; set; }
        [ForeignKey("ReferenceTypeID")]
        [InverseProperty("DocumentReferenceStatusMaps")]
        public virtual DocumentReferenceType ReferenceType { get; set; }
        [InverseProperty("DocumentStatus")]
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        [InverseProperty("DocumentStatus")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
