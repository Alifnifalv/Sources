namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LibraryTransactionTypes", Schema = "schools")]
    public partial class LibraryTransactionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LibraryTransactionType()
        {
            LibraryTransactions = new HashSet<LibraryTransaction>();
        }
        [Key]
        public byte LibraryTransactionTypeID { get; set; }

        [StringLength(50)]
        public string TransactionTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
    }
}
