using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AcademicNotes", Schema = "schools")]
    public partial class AcademicNote
    {
        [Key]
        public long AcademicNoteIID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }
        public int? SubjectID { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ClassID")]
        [InverseProperty("AcademicNotes")]
        public virtual Class Class { get; set; }
        [ForeignKey("SectionID")]
        [InverseProperty("AcademicNotes")]
        public virtual Section Section { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("AcademicNotes")]
        public virtual Subject Subject { get; set; }
    }
}
