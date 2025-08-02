using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.School.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CounselorHubAttachmentMaps", Schema = "Counseling")]
    public partial class CounselorHubAttachmentMap
    {
        [Key]
        public long CounselorHubAttachmentMapIID { get; set; }
        public long? CounselorHubID { get; set; }
        public long? AttachmentReferenceID { get; set; }
        [StringLength(50)]
        public string AttachmentName { get; set; }
        [StringLength(1000)]
        public string AttachmentDescription { get; set; }
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? StudentID { get; set; }


        [ForeignKey("CounselorHubID")]
        [InverseProperty("CounselorHubAttachmentMaps")]
        public virtual CounselorHub CounselorHub { get; set; }

        [ForeignKey("StudentID")]
        [InverseProperty("CounselorHubAttachmentMaps")]
        public virtual Student Student { get; set; }
    }
}
