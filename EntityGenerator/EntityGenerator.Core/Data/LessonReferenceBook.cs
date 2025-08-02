using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonReferenceBooks", Schema = "schools")]
    public partial class LessonReferenceBook
    {
        [Key]
        public int LessonReferenceBookID { get; set; }
        public int LessonID { get; set; }
        [StringLength(50)]
        public string BookTitle { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? PublicationYear { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
