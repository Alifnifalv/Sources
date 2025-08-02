using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PeriodClosingTranHeads", Schema = "account")]
    public partial class PeriodClosingTranHead
    {
        public PeriodClosingTranHead()
        {
            PeriodClosingTranTails = new HashSet<PeriodClosingTranTail>();
        }

        [Key]
        public long PeriodClosingTranHeadIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public int? PeriodClosingTranStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PeriodClosingTranStatusID")]
        [InverseProperty("PeriodClosingTranHeads")]
        public virtual PeriodClosingTranStatus PeriodClosingTranStatus { get; set; }
        [InverseProperty("PeriodClosingTranHead")]
        public virtual ICollection<PeriodClosingTranTail> PeriodClosingTranTails { get; set; }
    }
}
