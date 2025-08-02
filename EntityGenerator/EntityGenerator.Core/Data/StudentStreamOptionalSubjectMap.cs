using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentStreamOptionalSubjectMaps", Schema = "schools")]
    public partial class StudentStreamOptionalSubjectMap
    {
        [Key]
        public long StudentStreamOptionalSubjectMapIID { get; set; }
        public byte? StreamID { get; set; }
        public int? SubjectID { get; set; }
        public long? StudentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("StreamID")]
        [InverseProperty("StudentStreamOptionalSubjectMaps")]
        public virtual Stream Stream { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentStreamOptionalSubjectMaps")]
        public virtual Student Student { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("StudentStreamOptionalSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
