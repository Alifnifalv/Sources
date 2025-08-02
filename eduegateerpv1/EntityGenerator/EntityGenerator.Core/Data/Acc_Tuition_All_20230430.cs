using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Acc_Tuition_All_20230430
    {
        [Required]
        [StringLength(28)]
        [Unicode(false)]
        public string FeeName { get; set; }
        public long StudentID { get; set; }
        public int AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? OpBal { get; set; }
        [Column(TypeName = "money")]
        public decimal? TrBal { get; set; }
        [Column(TypeName = "money")]
        public decimal? Acc_OS { get; set; }
    }
}
