namespace Eduegate.Domain.Entity.Setting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ClaimSets", Schema = "admin")]
    public partial class ClaimSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClaimSet()
        {
            ClaimSetClaimMaps = new HashSet<ClaimSetClaimMap>();
            ClaimSetClaimSetMapClaimSets = new HashSet<ClaimSetClaimSetMap>();
            ClaimSetClaimSetMapLinkedClaimSets = new HashSet<ClaimSetClaimSetMap>();
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

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        //[InverseProperty("ClaimSet")]
        public virtual ICollection<ClaimSetClaimMap> ClaimSetClaimMaps { get; set; }
        //[InverseProperty("ClaimSet")]
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMapClaimSets { get; set; }
        //[InverseProperty("LinkedClaimSet")]
        public virtual ICollection<ClaimSetClaimSetMap> ClaimSetClaimSetMapLinkedClaimSets { get; set; }
        //[InverseProperty("ClaimSet")]
        public virtual ICollection<ClaimSetLoginMap> ClaimSetLoginMaps { get; set; }
    }
}
