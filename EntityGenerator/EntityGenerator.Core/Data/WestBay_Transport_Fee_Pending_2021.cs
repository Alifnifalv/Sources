using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class WestBay_Transport_Fee_Pending_2021
    {
        [StringLength(255)]
        public string Student { get; set; }
        [StringLength(255)]
        public string FeeName { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fee_Due { get; set; }
        [StringLength(255)]
        public string Cr_Note { get; set; }
        [StringLength(255)]
        public string Fee_Coll { get; set; }
        [Column(TypeName = "money")]
        public decimal? Fee_Bal { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [StringLength(255)]
        public string F8 { get; set; }
    }
}
