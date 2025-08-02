using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Courses", Schema = "schools")]
    public partial class Cours
    {
        [Key]
        public int CourseID { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string CourseName { get; set; }
        public int CategoryID { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        public int Duration { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string Level { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string ImageURL { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("Cours")]
        public virtual Subject Category { get; set; }
    }
}
