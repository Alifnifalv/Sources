namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin.ClaimSets")]
    public partial class ClaimSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClaimSet()
        {
            ClaimSetClaimMaps = new HashSet<ClaimSetClaimMap>();
            ClaimSetClaimSetMaps = new HashSet<ClaimSetClaimSetMap>();
            ClaimSetClaimSetMaps1 = new HashSet<ClaimSetClaimSetMap>();
            ClaimSetLoginMaps = new HashSet<ClaimSetLoginMap>();
        }

        [Key]
        public long ClaimSetIID { get; set; }

        [StringLength(50)]
        public string ClaimSetName { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMaps1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
    }
}
