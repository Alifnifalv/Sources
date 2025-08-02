namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.AdmissionEnquiries")]
    public partial class AdmissionEnquiry
    {
        [Key]
        public long AdmissionEnquiryIID { get; set; }

        [StringLength(50)]
        public string PersonName { get; set; }

        [StringLength(30)]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? NextFollowUpDate { get; set; }

        [StringLength(50)]
        public string Assinged { get; set; }

        public byte? ReferenceTypeID { get; set; }

        public byte? SourceID { get; set; }

        public int? ClassID { get; set; }

        public int? NumberOfChild { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public virtual School School { get; set; }
    }
}
