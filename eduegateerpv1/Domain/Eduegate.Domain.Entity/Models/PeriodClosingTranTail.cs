using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Eduegate.Domain.Entity.Models
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual PeriodClosingTranHead PeriodClosingTranHead { get; set; }

        public virtual PeriodClosingTranMaster PeriodClosingTranMaster { get; set; }
    }
}
