using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StreamSubjectMaps", Schema = "schools")]
    public partial class StreamSubjectMap
    {
        [Key]
        public long StreamSubjectMapIID { get; set; }
        public byte? StreamID { get; set; }
        public int? SubjectID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsOptionalSubject { get; set; }
        public int? OrderBy { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StreamSubjectMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StreamSubjectMaps")]
        public virtual School School { get; set; }
        [ForeignKey("StreamID")]
        [InverseProperty("StreamSubjectMaps")]
        public virtual Stream Stream { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("StreamSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
