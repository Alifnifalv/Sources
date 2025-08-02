using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.School.Models
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

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Employee Teacher { get; set; }

        public virtual TimeTableExtension TimeTableExt { get; set; }
    }
}
