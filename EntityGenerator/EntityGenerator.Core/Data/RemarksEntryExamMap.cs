using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RemarksEntryExamMap", Schema = "schools")]
    public partial class RemarksEntryExamMap
    {
        [Key]
        public long RemarksEntryExamMapIID { get; set; }
        public long? ExamID { get; set; }
        public int? subjectID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public long? RemarksEntryStudentMapID { get; set; }
        public string Remarks { get; set; }

        [ForeignKey("ExamID")]
        [InverseProperty("RemarksEntryExamMaps")]
        public virtual Exam Exam { get; set; }
        [ForeignKey("RemarksEntryStudentMapID")]
        [InverseProperty("RemarksEntryExamMaps")]
        public virtual RemarksEntryStudentMap RemarksEntryStudentMap { get; set; }
        [ForeignKey("subjectID")]
        [InverseProperty("RemarksEntryExamMaps")]
        public virtual Subject subject { get; set; }
    }
}
