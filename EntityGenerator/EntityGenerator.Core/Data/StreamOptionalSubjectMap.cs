using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StreamOptionalSubjectMaps", Schema = "schools")]
    public partial class StreamOptionalSubjectMap
    {
        [Key]
        public long StreamOptionalSubjectIID { get; set; }
        public byte? StreamID { get; set; }
        public long? StreamSubjectMapID { get; set; }
        public int? SubjectID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? OrderBy { get; set; }

        [ForeignKey("StreamID")]
        [InverseProperty("StreamOptionalSubjectMaps")]
        public virtual Stream Stream { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("StreamOptionalSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
