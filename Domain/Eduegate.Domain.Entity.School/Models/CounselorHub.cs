using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("CounselorHubs", Schema = "Counseling")]
    public partial class CounselorHub
    {
        public CounselorHub()
        {
            CounselorHubMaps = new HashSet<CounselorHubMap>();
        }

        [Key]
        public long CounselorHubIID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        [StringLength(500)]
        public string ShortTitle { get; set; }
        public string Message { get; set; }
        public byte? CircularStatusID { get; set; }
        public bool? IsSendNotification { get; set; }
        public byte? SchoolID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        public DateTime? CounselorHubEntryDate { get; set; }

        public DateTime? CounselorHubExpiryDate { get; set; }

        public byte? CounselorHubStatusID { get; set; }


        [ForeignKey("AcademicYearID")]
        [InverseProperty("CounselorHubs")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("CounselorHubs")]
        public virtual Schools School { get; set; }
        [InverseProperty("CounselorHub")]
        public virtual ICollection<CounselorHubMap> CounselorHubMaps { get; set; }
        public virtual CounselorHubStatus CounselorHubStatus { get; set; }

        public virtual ICollection<CounselorHubAttachmentMap> CounselorHubAttachmentMaps { get; set; }


    }
}
