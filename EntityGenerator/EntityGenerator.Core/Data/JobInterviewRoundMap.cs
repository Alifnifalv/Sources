using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobInterviewRoundMaps", Schema = "hr")]
    public partial class JobInterviewRoundMap
    {
        [Key]
        public long RoundMapID { get; set; }
        public long? InterviewID { get; set; }
        public int? RoundID { get; set; }

        [ForeignKey("InterviewID")]
        [InverseProperty("JobInterviewRoundMaps")]
        public virtual JobInterview Interview { get; set; }
        [ForeignKey("RoundID")]
        [InverseProperty("JobInterviewRoundMaps")]
        public virtual JobInterviewRound Round { get; set; }
    }
}
