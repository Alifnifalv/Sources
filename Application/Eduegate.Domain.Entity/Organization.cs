namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("communities.Organizations")]
    public partial class Organization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organization()
        {
            Organizations1 = new HashSet<Organization>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrganizationID { get; set; }

        [StringLength(100)]
        public string OrganizationName { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        public int? ParentOrganizationID { get; set; }

        [StringLength(50)]
        public string RegistrationID { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Organization> Organizations1 { get; set; }

        public virtual Organization Organization1 { get; set; }
    }
}
