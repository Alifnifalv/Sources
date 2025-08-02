using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SubjectTypes", Schema = "schools")]
    public partial class SubjectType
    {
        public SubjectType()
        {
            Subjects = new HashSet<Subject>();
            TimeTableExtensions = new HashSet<TimeTableExtension>();
        }

        [Key]
        public byte SubjectTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

        [InverseProperty("SubjectType")]
        public virtual ICollection<Subject> Subjects { get; set; }
        [InverseProperty("SubjectType")]
        public virtual ICollection<TimeTableExtension> TimeTableExtensions { get; set; }
    }
}
