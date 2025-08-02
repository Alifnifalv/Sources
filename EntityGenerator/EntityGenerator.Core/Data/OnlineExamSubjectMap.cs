using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamSubjectMaps", Schema = "exam")]
    public partial class OnlineExamSubjectMap
    {
        [Key]
        public long OnlineExamSubjectMapIID { get; set; }
        public long? OnlineExamID { get; set; }
        public int? SubjectID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("OnlineExamID")]
        [InverseProperty("OnlineExamSubjectMaps")]
        public virtual OnlineExam OnlineExam { get; set; }
        [ForeignKey("SubjectID")]
        [InverseProperty("OnlineExamSubjectMaps")]
        public virtual Subject Subject { get; set; }
    }
}
