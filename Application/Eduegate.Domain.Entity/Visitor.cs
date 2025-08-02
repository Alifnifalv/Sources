namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Visitors")]
    public partial class Visitor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Visitor()
        {
            VisitorAttachmentMaps = new HashSet<VisitorAttachmentMap>();
        }

        [Key]
        public long VisitorIID { get; set; }

        [StringLength(50)]
        public string VisitorNumber { get; set; }

        [StringLength(50)]
        public string QID { get; set; }

        [StringLength(50)]
        public string PassportNumber { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string MobileNumber1 { get; set; }

        [StringLength(20)]
        public string MobileNumber2 { get; set; }

        [StringLength(100)]
        public string EmailID { get; set; }

        public long? LoginID { get; set; }

        public string OtherDetails { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Login Login { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitorAttachmentMap> VisitorAttachmentMaps { get; set; }
    }
}
