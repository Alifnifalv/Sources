using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Visitors", Schema = "mutual")]
    public partial class Visitor
    {
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("LoginID")]
        [InverseProperty("Visitors")]
        public virtual Login Login { get; set; }
        [InverseProperty("Visitor")]
        public virtual ICollection<VisitorAttachmentMap> VisitorAttachmentMaps { get; set; }
    }
}
