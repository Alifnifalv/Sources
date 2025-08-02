using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("EmployeesImportNew")]
    public partial class EmployeesImportNew
    {
        public double? F1 { get; set; }
        public double? F2 { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [Column("Desig ")]
        [StringLength(255)]
        public string Desig_ { get; set; }
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
        public double? Basic { get; set; }
        public double? HRA { get; set; }
        public double? TransAll { get; set; }
        public double? OtherAll { get; set; }
        public double? FunAll { get; set; }
        public double? Total { get; set; }
        public double? LSDays { get; set; }
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
