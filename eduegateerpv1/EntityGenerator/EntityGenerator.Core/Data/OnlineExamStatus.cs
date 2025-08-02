using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamStatuses", Schema = "exam")]
    public partial class OnlineExamStatus
    {
        public OnlineExamStatus()
        {
            CandidateOnlineExamMaps = new HashSet<CandidateOnlineExamMap>();
        }

        [Key]
        public byte ExamStatusID { get; set; }
        [StringLength(50)]
        public string ExamStatusName { get; set; }

        [InverseProperty("OnlineExamStatus")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
    }
}
