using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTableExtTeachers", Schema = "schools")]
    public partial class TimeTableExtTeacher
    {
        [Key]
        public long TimeTableExtTeacherIID { get; set; }
        public long TimeTableExtID { get; set; }
        public int? SubjectID { get; set; }
        public long TeacherID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("SubjectID")]
        [InverseProperty("TimeTableExtTeachers")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TeacherID")]
        [InverseProperty("TimeTableExtTeachers")]
        public virtual Employee Teacher { get; set; }
        [ForeignKey("TimeTableExtID")]
        [InverseProperty("TimeTableExtTeachers")]
        public virtual TimeTableExtension TimeTableExt { get; set; }
    }
}
