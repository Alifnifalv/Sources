using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("PrvBalance")]
    public partial class PrvBalance
    {
        public long? Main_GroupID { get; set; }
        public long? Sub_GroupID { get; set; }
        public long? GroupID { get; set; }
        public long AccountID { get; set; }
        [Column(TypeName = "money")]
        public decimal? OpBal { get; set; }
        [Column(TypeName = "money")]
        public decimal? TrBal { get; set; }
        [Column(TypeName = "money")]
        public decimal? ClBal { get; set; }
    }
}
