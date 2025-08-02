namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("RemarksEntryExamMap", Schema = "schools")]
    public partial class RemarksEntryExamMap
    {
        [Key]
        public long RemarksEntryExamMapIID { get; set; }

        public long? ExamID { get; set; }

        public int? subjectID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public long? RemarksEntryStudentMapID { get; set; }

        public string Remarks { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual RemarksEntryStudentMap RemarksEntryStudentMap { get; set; }
    }
}

