using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PollID")]
        [InverseProperty("SchoolPollAnswerLogs")]
        public virtual Poll Poll { get; set; }
        [ForeignKey("PollAnswerMapID")]
        [InverseProperty("SchoolPollAnswerLogs")]
        public virtual PollAnswerMap PollAnswerMap { get; set; }
        [ForeignKey("StaffID")]
        [InverseProperty("SchoolPollAnswerLogs")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("SchoolPollAnswerLogs")]
        public virtual Student Student { get; set; }
    }
}
