using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CounselorHubMaps", Schema = "Counseling")]
    public partial class CounselorHubMap
    {
        [Key]
        public long CounselorHubMapIID { get; set; }
        public long? CounselorHubID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public long? StudentID { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public bool? AllClass { get; set; }
        public bool? AllSection { get; set; }
        public bool? AllStudent { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("CounselorHubMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("CounselorHubID")]
        [InverseProperty("CounselorHubMaps")]
        public virtual CounselorHub CounselorHub { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("CounselorHubMaps")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("CounselorHubMaps")]
        public virtual Student Student { get; set; }
    }
}
