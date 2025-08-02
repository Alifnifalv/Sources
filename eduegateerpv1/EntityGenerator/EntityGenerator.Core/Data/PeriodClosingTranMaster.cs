using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PeriodClosingTranMasters", Schema = "account")]
    public partial class PeriodClosingTranMaster
    {
        public PeriodClosingTranMaster()
        {
            PeriodClosingTranTails = new HashSet<PeriodClosingTranTail>();
        }

        [Key]
        public int PeriodClosingTranMasterID { get; set; }
        [StringLength(100)]
        public string TableName { get; set; }
        [StringLength(100)]
        public string ColumnName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("PeriodClosingTranMaster")]
        public virtual ICollection<PeriodClosingTranTail> PeriodClosingTranTails { get; set; }
    }
}
