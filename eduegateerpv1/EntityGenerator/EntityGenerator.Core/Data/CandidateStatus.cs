using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CandidateStatuses", Schema = "exam")]
    public partial class CandidateStatus
    {
        public CandidateStatus()
        {
            Candidates = new HashSet<Candidate>();
        }

        [Key]
        public byte CandidateStatusID { get; set; }
        [StringLength(50)]
        public string CandidateStatusName { get; set; }

        [InverseProperty("CandidateStatus")]
        public virtual ICollection<Candidate> Candidates { get; set; }
    }
}
