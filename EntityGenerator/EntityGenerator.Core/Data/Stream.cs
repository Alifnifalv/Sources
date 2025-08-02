using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Streams", Schema = "schools")]
    public partial class Stream
    {
        public Stream()
        {
            StreamOptionalSubjectMaps = new HashSet<StreamOptionalSubjectMap>();
            StreamSubjectMaps = new HashSet<StreamSubjectMap>();
            StudentApplicationOptionalSubjectMaps = new HashSet<StudentApplicationOptionalSubjectMap>();
            StudentApplications = new HashSet<StudentApplication>();
            StudentStreamOptionalSubjectMaps = new HashSet<StudentStreamOptionalSubjectMap>();
            Students = new HashSet<Student>();
        }

        [Key]
        public byte StreamID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public byte? SchoolID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? StreamGroupID { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Streams")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Streams")]
        public virtual School School { get; set; }
        [ForeignKey("StreamGroupID")]
        [InverseProperty("Streams")]
        public virtual StreamGroup StreamGroup { get; set; }
        [InverseProperty("Stream")]
        public virtual ICollection<StreamOptionalSubjectMap> StreamOptionalSubjectMaps { get; set; }
        [InverseProperty("Stream")]
        public virtual ICollection<StreamSubjectMap> StreamSubjectMaps { get; set; }
        [InverseProperty("Stream")]
        public virtual ICollection<StudentApplicationOptionalSubjectMap> StudentApplicationOptionalSubjectMaps { get; set; }
        [InverseProperty("Stream")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        [InverseProperty("Stream")]
        public virtual ICollection<StudentStreamOptionalSubjectMap> StudentStreamOptionalSubjectMaps { get; set; }
        [InverseProperty("Stream")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
