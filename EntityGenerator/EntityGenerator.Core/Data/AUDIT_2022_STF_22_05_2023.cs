using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AUDIT_2022_STF_22_05_2023
    {
        [Column("SI #")]
        public double? SI__ { get; set; }
        [Column("Location ")]
        [StringLength(255)]
        public string Location_ { get; set; }
        [Column("Product Code")]
        [StringLength(255)]
        public string Product_Code { get; set; }
        [Column("Product Name")]
        [StringLength(255)]
        public string Product_Name { get; set; }
        [Column("Op Stock")]
        public double? Op_Stock { get; set; }
        [Column("Op Stock Value")]
        public double? Op_Stock_Value { get; set; }
        [Column("Tr Stock")]
        public double? Tr_Stock { get; set; }
        [Column("Tr Stock Value")]
        public double? Tr_Stock_Value { get; set; }
        [Column("Cl Stock")]
        public double? Cl_Stock { get; set; }
        [Column("Cl Stock Value")]
        public double? Cl_Stock_Value { get; set; }
        [Column("Physical Count")]
        public double? Physical_Count { get; set; }
        public double? Difference { get; set; }
    }
}
