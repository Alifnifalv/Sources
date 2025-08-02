using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PeriodClosingTranStatuses", Schema = "account")]
    public partial class PeriodClosingTranStatus
    {
        public PeriodClosingTranStatus()
        {
            PeriodClosingTranHeads = new HashSet<PeriodClosingTranHead>();
        }

        [Key]
        public int PeriodClosingTranStatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("PeriodClosingTranStatus")]
        public virtual ICollection<PeriodClosingTranHead> PeriodClosingTranHeads { get; set; }
    }
}
