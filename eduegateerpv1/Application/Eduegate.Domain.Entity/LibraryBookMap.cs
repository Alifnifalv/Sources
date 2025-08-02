namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.LibraryBookMaps")]
    public partial class LibraryBookMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LibraryBookMap()
        {
            LibraryTransactions = new HashSet<LibraryTransaction>();
        }

        [Key]
        public int LibraryBookMapIID { get; set; }

        public long? LibraryBookID { get; set; }

        [StringLength(100)]
        public string Call_No { get; set; }

        [StringLength(100)]
        public string Acc_No { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual LibraryBook LibraryBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LibraryTransaction> LibraryTransactions { get; set; }
    }
}
