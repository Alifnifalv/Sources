using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TB_PS_Audit_2021
    {
        [Column("New Ledger Code")]
        [StringLength(500)]
        public string New_Ledger_Code { get; set; }
        [Column("General Ledger Code")]
        public double? General_Ledger_Code { get; set; }
        [Column("GL Name")]
        [StringLength(255)]
        public string GL_Name { get; set; }
        [StringLength(255)]
        public string Grouping { get; set; }
        [Column("Main Head")]
        [StringLength(255)]
        public string Main_Head { get; set; }
        [Column("Sub-Group")]
        [StringLength(255)]
        public string Sub_Group { get; set; }
        [Column("Income Tax Classification (Main)")]
        [StringLength(255)]
        public string Income_Tax_Classification__Main_ { get; set; }
        [Column("Income Tax Sub Classification")]
        [StringLength(255)]
        public string Income_Tax_Sub_Classification { get; set; }
        [Column("Presentation Name")]
        [StringLength(255)]
        public string Presentation_Name { get; set; }
        [Column("12/31/2021")]
        public double? _12_31_2021 { get; set; }
        [Column("12/31/2020")]
        public double? _12_31_2020 { get; set; }
        [Column("1/1/2020")]
        public double? _1_1_2020 { get; set; }
        public double? F12 { get; set; }
        [Column("GL Provided?")]
        public double? GL_Provided_ { get; set; }
        [StringLength(255)]
        public string F14 { get; set; }
    }
}
