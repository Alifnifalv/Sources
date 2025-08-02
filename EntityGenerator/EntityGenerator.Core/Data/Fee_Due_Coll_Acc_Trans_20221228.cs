using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Fee_Due_Coll_Acc_Trans_20221228
    {
        public int? Yr { get; set; }
        public long? StudentFeeDueID { get; set; }
        public long FeeDueFeeTypeMapsID { get; set; }
        public long? StudentId { get; set; }
        [Column(TypeName = "decimal(38, 3)")]
        public decimal? Due_Amount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Coll_Amount { get; set; }
        public int? Due_TH_ID { get; set; }
        public int? Coll_TH_ID { get; set; }
        public int? Crn_TH_ID { get; set; }
        public int? Due_Dr_Acc_ID { get; set; }
        public int? Due_Cr_Acc_ID { get; set; }
        [Column(TypeName = "money")]
        public decimal? Due_Acc_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Coll_Acc_Amount { get; set; }
        [Column(TypeName = "money")]
        public decimal? Crn_Acc_Amount { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string REMARKS { get; set; }
    }
}
