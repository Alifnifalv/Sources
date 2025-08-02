using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StreamGroups", Schema = "schools")]
    public partial class StreamGroup
    {
        public StreamGroup()
        {
            Streams = new HashSet<Stream>();
            StudentApplications = new HashSet<StudentApplication>();
        }

        [Key]
        public byte StreamGroupID { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StreamGroups")]
        public virtual AcademicYear AcademicYear { get; set; }
        [InverseProperty("StreamGroup")]
        public virtual ICollection<Stream> Streams { get; set; }
        [InverseProperty("StreamGroup")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
    }
}
