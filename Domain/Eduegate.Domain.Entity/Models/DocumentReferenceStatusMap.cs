using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("DocumentReferenceStatusMap", Schema = "mutual")]
    public partial class DocumentReferenceStatusMap
    {
        public DocumentReferenceStatusMap()
        {
            this.AssetTransactionHeads = new List<AssetTransactionHead>();
            this.TransactionHeads = new List<TransactionHead>();
        }

        [Key]
        public long DocumentReferenceStatusMapID { get; set; }
        public Nullable<int> ReferenceTypeID { get; set; }
        public long DocumentStatusID { get; set; }
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
        public virtual DocumentReferenceType DocumentReferenceType { get; set; }
        public virtual DocumentStatus DocumentStatus { get; set; }
    }
}
