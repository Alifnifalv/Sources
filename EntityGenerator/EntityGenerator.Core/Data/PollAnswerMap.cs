using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PollAnswerMaps", Schema = "collaboration")]
    public partial class PollAnswerMap
    {
        public PollAnswerMap()
        {
            SchoolPollAnswerLogs = new HashSet<SchoolPollAnswerLog>();
        }

        [Key]
        public long PollAnswerMapIID { get; set; }
        public long? PollID { get; set; }
        [StringLength(2000)]
        public string AnswerDescription { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("PollID")]
        [InverseProperty("PollAnswerMaps")]
        public virtual Poll Poll { get; set; }
        [InverseProperty("PollAnswerMap")]
        public virtual ICollection<SchoolPollAnswerLog> SchoolPollAnswerLogs { get; set; }
    }
}
