using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentApplicationOptionalSubjectMaps", Schema = "schools")]
    public partial class StudentApplicationOptionalSubjectMap
    {
        [Key]
        public long StudentApplicationOptionalSubjectMapIID { get; set; }
        public byte? StreamID { get; set; }
        public int? SubjectID { get; set; }
        public long? ApplicationID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("ApplicationID")]
        [InverseProperty("StudentApplicationOptionalSubjectMaps")]
        public virtual StudentApplication Application { get; set; }
        [ForeignKey("StreamID")]
        [InverseProperty("StudentApplicationOptionalSubjectMaps")]
        public virtual Stream Stream { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("StudentApplicationOptionalSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
