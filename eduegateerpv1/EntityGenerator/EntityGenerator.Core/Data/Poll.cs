using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Polls", Schema = "collaboration")]
    public partial class Poll
    {
        public Poll()
        {
            PollAnswerMaps = new HashSet<PollAnswerMap>();
            SchoolPollAnswerLogs = new HashSet<SchoolPollAnswerLog>();
        }

        [Key]
        public long PollIID { get; set; }
        [StringLength(500)]
        public string PollTitle { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpiryDate { get; set; }
        public bool? IsAllowOtherAnwser { get; set; }
        public bool? IsOpenForPolling { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("Poll")]
        public virtual ICollection<PollAnswerMap> PollAnswerMaps { get; set; }
        [InverseProperty("Poll")]
        public virtual ICollection<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }
    }
}
