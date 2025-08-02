using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AUDIT_2022_FTS_21_05_2023
    {
        [Column("SI #")]
        public double? SI__ { get; set; }
        [StringLength(255)]
        public string ITEMNAME { get; set; }
        [Column("Item no")]
        [StringLength(255)]
        public string Item_no { get; set; }
        public double? QUANTITY { get; set; }
        [Column("DTT Count")]
        public double? DTT_Count { get; set; }
        public double? Difference { get; set; }
        [StringLength(255)]
        public string Justification { get; set; }
        [StringLength(255)]
        public string F8 { get; set; }
        [StringLength(255)]
        public string F9 { get; set; }
        [StringLength(255)]
        public string F10 { get; set; }
    }
}
