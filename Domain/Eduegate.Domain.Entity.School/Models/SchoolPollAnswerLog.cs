namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SchoolPollAnswerLogs", Schema = "schools")]
    public partial class SchoolPollAnswerLog
    {
        [Key]
        public long SchoolPollAnswerLogIID { get; set; }

        public long? PollID { get; set; }

        public long? PollAnswerMapID { get; set; }

        public long? StudentID { get; set; }

        public long? StaffID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual PollAnswerMap PollAnswerMap { get; set; }

        public virtual Poll Poll { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Student Student { get; set; }
    }
}
