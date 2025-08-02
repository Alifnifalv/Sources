namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin.ClaimTypes")]
    public partial class ClaimType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClaimType()
        {
            Claims = new HashSet<Claim>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClaimTypeID { get; set; }

        [StringLength(50)]
        public string ClaimTypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Claim> Claims { get; set; }
    }
}
