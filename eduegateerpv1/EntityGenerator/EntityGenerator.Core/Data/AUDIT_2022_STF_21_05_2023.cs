using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AUDIT_2022_STF_21_05_2023
    {
        [Column("SI #")]
        public double? SI__ { get; set; }
        [StringLength(255)]
        public string ITEMNAME { get; set; }
        [Column("Item Code")]
        [StringLength(255)]
        public string Item_Code { get; set; }
        public double? QUANTITY { get; set; }
        [Column("DTT Count")]
        public double? DTT_Count { get; set; }
        public double? Difference { get; set; }
        [StringLength(255)]
        public string Justification { get; set; }
        [StringLength(255)]
        public string F8 { get; set; }
    }
}
