using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PeriodClosingTranTails", Schema = "account")]
    public partial class PeriodClosingTranTail
    {
        [Key]
        public long PeriodClosingTranTailIID { get; set; }
        public long? PeriodClosingTranHeadID { get; set; }
        public int? PeriodClosingTranMasterID { get; set; }
        public int? DataValue { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PeriodClosingTranHeadID")]
        [InverseProperty("PeriodClosingTranTails")]
        public virtual PeriodClosingTranHead PeriodClosingTranHead { get; set; }
        [ForeignKey("PeriodClosingTranMasterID")]
        [InverseProperty("PeriodClosingTranTails")]
        public virtual PeriodClosingTranMaster PeriodClosingTranMaster { get; set; }
    }
}
