using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("EmployeesImport")]
    public partial class EmployeesImport
    {
        public double? F1 { get; set; }
        public double? F2 { get; set; }
        [StringLength(255)]
        public string F3 { get; set; }
        [Column(" ")]
        [StringLength(255)]
        public string _ { get; set; }
        [StringLength(255)]
        public string F5 { get; set; }
        [StringLength(255)]
        public string F6 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? F7 { get; set; }
        [StringLength(255)]
        public string F8 { get; set; }
        [StringLength(255)]
        public string F9 { get; set; }
        [StringLength(255)]
        public string F10 { get; set; }
        [StringLength(255)]
        public string F11 { get; set; }
        public double? F12 { get; set; }
        public double? F13 { get; set; }
        [StringLength(255)]
        public string F14 { get; set; }
        public double? F15 { get; set; }
        [StringLength(255)]
        public string F16 { get; set; }
        public double? F17 { get; set; }
        [StringLength(255)]
        public string F18 { get; set; }
        public double? F19 { get; set; }
        [StringLength(255)]
        public string F20 { get; set; }
        [StringLength(255)]
        public string F21 { get; set; }
        [StringLength(255)]
        public string F22 { get; set; }
        [StringLength(255)]
        public string F23 { get; set; }
        [StringLength(255)]
        public string F24 { get; set; }
        [StringLength(255)]
        public string F25 { get; set; }
        [StringLength(255)]
        public string F26 { get; set; }
        [StringLength(255)]
        public string F27 { get; set; }
        public double? F28 { get; set; }
        [StringLength(255)]
        public string F29 { get; set; }
        [StringLength(255)]
        public string F30 { get; set; }
        [StringLength(255)]
        public string F31 { get; set; }
        [StringLength(255)]
        public string F32 { get; set; }
        [StringLength(255)]
        public string F33 { get; set; }
        [StringLength(255)]
        public string F34 { get; set; }
        [StringLength(255)]
        public string F35 { get; set; }
        [StringLength(255)]
        public string F36 { get; set; }
        [StringLength(255)]
        public string F37 { get; set; }
        [StringLength(255)]
        public string F38 { get; set; }
        [StringLength(255)]
        public string F39 { get; set; }
        [StringLength(255)]
        public string F40 { get; set; }
        [StringLength(255)]
        public string F41 { get; set; }
        [StringLength(255)]
        public string F42 { get; set; }
        [StringLength(255)]
        public string F43 { get; set; }
        [StringLength(255)]
        public string F44 { get; set; }
        [StringLength(255)]
        public string F45 { get; set; }
    }
}
