using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OS_Tuition_Fee_Class_Summary_20221109
    {
        [StringLength(255)]
        public string Grade { get; set; }
        [StringLength(255)]
        public string Section { get; set; }
        public double? April_2021 { get; set; }
        public double? May_2021 { get; set; }
        public double? June_2021 { get; set; }
        public double? July_2021 { get; set; }
        public double? August_2021 { get; set; }
        public double? September_2021 { get; set; }
        public double? October_2021 { get; set; }
        public double? November_2021 { get; set; }
        public double? December_2021 { get; set; }
        public double? Total { get; set; }
        public double? OB_April_2021 { get; set; }
        public double? OB_June_2021 { get; set; }
        public double? OB_Total { get; set; }
        [StringLength(255)]
        public string F16 { get; set; }
        [StringLength(255)]
        public string F17 { get; set; }
    }
}
