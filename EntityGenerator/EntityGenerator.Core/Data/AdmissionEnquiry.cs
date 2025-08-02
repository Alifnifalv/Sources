using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AdmissionEnquiries", Schema = "schools")]
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
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NextFollowUpDate { get; set; }
        [StringLength(50)]
        public string Assinged { get; set; }
        public byte? ReferenceTypeID { get; set; }
        public byte? SourceID { get; set; }
        public int? ClassID { get; set; }
        public int? NumberOfChild { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }

        [ForeignKey("SchoolID")]
        [InverseProperty("AdmissionEnquiries")]
        public virtual School School { get; set; }
    }
}
