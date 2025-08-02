using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OnlineExamOperationStatuses", Schema = "exam")]
    public partial class OnlineExamOperationStatus
    {
        public OnlineExamOperationStatus()
        {
            CandidateOnlineExamMaps = new HashSet<CandidateOnlineExamMap>();
        }

        [Key]
        public byte OnlineExamOperationStatusID { get; set; }
        [StringLength(50)]
        public string OperationStatus { get; set; }

        [InverseProperty("OnlineExamOperationStatus")]
        public virtual ICollection<CandidateOnlineExamMap> CandidateOnlineExamMaps { get; set; }
    }
}
