using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PromotionStatuses", Schema = "schools")]
    public partial class PromotionStatus
    {
        public PromotionStatus()
        {
            StudentPromotionLogs = new HashSet<StudentPromotionLog>();
        }

        [Key]
        public byte PromotionStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("PromotionStatus")]
        public virtual ICollection<StudentPromotionLog> StudentPromotionLogs { get; set; }
    }
}
