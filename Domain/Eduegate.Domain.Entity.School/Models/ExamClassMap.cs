using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("ExamClassMaps", Schema = "schools")]
    public partial class ExamClassMap
    {
        [Key]
        public long ExamClassMapIID { get; set; }

        public long? ExamScheduleID { get; set; }

        public long? ExamID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Class Class { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual ExamSchedule ExamSchedule { get; set; }

        public virtual Section Section { get; set; }
    }
}