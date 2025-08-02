using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Podar_Fixed_Assets_Register
    {
        public short DataIID { get; set; }
        public double? Sr_No { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        [StringLength(200)]
        public string AssetDescription { get; set; }
        [StringLength(50)]
        public string Reference { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        [StringLength(50)]
        public string Bill_No { get; set; }
        [StringLength(50)]
        public string Qty { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date_of_Purchase { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date_of_Dep { get; set; }
        [StringLength(50)]
        public string DepLocation { get; set; }
        [StringLength(50)]
        public string Dept { get; set; }
        [StringLength(50)]
        public string AssetUser { get; set; }
        public double? COST_As_on_01_01_2023 { get; set; }
        public double? COST_Addition { get; set; }
        [StringLength(50)]
        public string COST_Disposal { get; set; }
        public double? COST_As_on_31_12_2023 { get; set; }
        public double? Depreciation_As_on_01_01_2023 { get; set; }
        public double? Depreciation_Dep_for_the_period { get; set; }
        [StringLength(50)]
        public string Depreciation_On_Disposal { get; set; }
        public double? Depreciation_As_on_31_12_2023 { get; set; }
        public double? Net_Value_31_12_2023 { get; set; }
        public double? Net_Value_31_12_2022 { get; set; }
        [StringLength(50)]
        public string Remark1 { get; set; }
        [StringLength(50)]
        public string Remark2 { get; set; }
        [StringLength(50)]
        public string Remark3 { get; set; }
        [StringLength(50)]
        public string Remark4 { get; set; }
    }
}
