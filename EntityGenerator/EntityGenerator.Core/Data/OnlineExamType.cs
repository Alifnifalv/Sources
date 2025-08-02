using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamTypes", Schema = "exam")]
    public partial class OnlineExamType
    {
        public OnlineExamType()
        {
            OnlineExams = new HashSet<OnlineExam>();
        }

        [Key]
        public byte ExamTypeID { get; set; }
        [StringLength(50)]
        public string ExamTypeName { get; set; }

        [InverseProperty("OnlineExamType")]
        public virtual ICollection<OnlineExam> OnlineExams { get; set; }
    }
}
