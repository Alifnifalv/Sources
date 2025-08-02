using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LessonResources", Schema = "schools")]
    public partial class LessonResource
    {
        [Key]
        public int LessonResourceID { get; set; }
        public int LessonID { get; set; }
        [StringLength(50)]
        public string ResourceTitle { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
