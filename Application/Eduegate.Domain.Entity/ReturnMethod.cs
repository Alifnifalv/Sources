namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.ReturnMethods")]
    public partial class ReturnMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ReturnMethod()
        {
            Suppliers = new HashSet<Supplier>();
            TransactionHeads = new HashSet<TransactionHead>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReturnMethodID { get; set; }

        [StringLength(100)]
        public string ReturnMethodName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier> Suppliers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionHead> TransactionHeads { get; set; }
    }
}
