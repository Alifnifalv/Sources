using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.School.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CounselorHubStatuses", Schema = "Counseling")]
    public partial class CounselorHubStatus
    {
        public CounselorHubStatus()
        {
            CounselorHubs = new HashSet<CounselorHub>();
        }

        [Key]
        public byte CounselorHubStatusID { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("CounselorHubStatus")]
        public virtual ICollection<CounselorHub> CounselorHubs { get; set; }
    }
}
