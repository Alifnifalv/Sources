using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("JobInterviewMarkMaps", Schema = "hr")]
    public partial class JobInterviewMarkMap
    {
        [Key]
        public long MarkMapID { get; set; }
        public long? InterviewID { get; set; }
        public long? ApplicantID { get; set; }
        public int? RoundID { get; set; }
        public int? Rating { get; set; }
        [StringLength(250)]
        public string Grade { get; set; }
        public string Remarks { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HeldOnDate { get; set; }
    }
}
