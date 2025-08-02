namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DocumentReferenceStatusMap", Schema = "mutual")]
    public partial class DocumentReferenceStatusMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentReferenceStatusMap()
        {
            AssetTransactionHeads = new HashSet<AssetTransactionHead>();
            TransactionHeads = new HashSet<TransactionHead>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DocumentReferenceStatusMapID { get; set; }

        public int? ReferenceTypeID { get; set; }

        public long DocumentStatusID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssetTransactionHead> AssetTransactionHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }

        //public virtual DocumentReferenceType DocumentReferenceType { get; set; }

        public virtual DocumentStatus DocumentStatus { get; set; }
    }
}
