namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("admin.Claims")]
    public partial class Claim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Claim()
        {
            ClaimLoginMaps = new HashSet<ClaimLoginMap>();
            ClaimSetClaimMaps = new HashSet<ClaimSetClaimMap>();
        }

        [Key]
        public long ClaimIID { get; set; }

        [StringLength(500)]
        public string ClaimName { get; set; }

        [StringLength(50)]
        public string ResourceName { get; set; }

        public int? ClaimTypeID { get; set; }

        [StringLength(50)]
        public string Rights { get; set; }

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
        public virtual ICollection<ClaimLoginMap> ClaimLoginMaps { get; set; }

        public virtual ClaimType ClaimType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
    }
}
