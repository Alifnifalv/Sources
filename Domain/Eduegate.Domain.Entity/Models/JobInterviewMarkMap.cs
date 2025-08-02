using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("JobInterviewMarkMaps", Schema = "hr")]
    public partial class JobInterviewMarkMap
    {
        public JobInterviewMarkMap()
        {

        }

        [Key]
        public long MarkMapID { get; set; }
        public long? InterviewID { get; set; }
        public long? ApplicantID { get; set; }
        public int? RoundID { get; set; }
        public int? Rating { get; set; }
        public string Grade { get; set; }
        public string Remarks { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? HeldOnDate { get; set; }

    }
}
