using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TimeTableExtSubjects", Schema = "schools")]
    public partial class TimeTableExtSubject
    {
        [Key]
        public long TimeTableExtSubjectIID { get; set; }
        public long TimeTableExtID { get; set; }
        public int SubjectID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("SubjectID")]
        [InverseProperty("TimeTableExtSubjects")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("TimeTableExtID")]
        [InverseProperty("TimeTableExtSubjects")]
        public virtual TimeTableExtension TimeTableExt { get; set; }
    }
}
