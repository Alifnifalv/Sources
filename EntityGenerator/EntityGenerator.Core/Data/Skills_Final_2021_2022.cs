using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Skills_Final_2021_2022
    {
        [Column("Sr No")]
        [StringLength(255)]
        public string Sr_No { get; set; }
        [Column("Academic Year")]
        [StringLength(255)]
        public string Academic_Year { get; set; }
        [StringLength(255)]
        public string Phase { get; set; }
        [StringLength(255)]
        public string Class { get; set; }
        [StringLength(255)]
        public string Section { get; set; }
        [Column("Enroll No")]
        [StringLength(255)]
        public string Enroll_No { get; set; }
        [Column("Student Name")]
        [StringLength(255)]
        public string Student_Name { get; set; }
        [StringLength(255)]
        public string Gender { get; set; }
        [StringLength(255)]
        public string Religion { get; set; }
        [StringLength(255)]
        public string Nationality { get; set; }
        [StringLength(255)]
        public string SEN { get; set; }
        [Column("G&T")]
        [StringLength(255)]
        public string G_T { get; set; }
        [StringLength(255)]
        public string Arab { get; set; }
        [StringLength(255)]
        public string Emirati { get; set; }
        [StringLength(255)]
        public string Group { get; set; }
        [StringLength(255)]
        public string Attribute { get; set; }
        [StringLength(255)]
        public string Indicator { get; set; }
        [StringLength(255)]
        public string Frequency { get; set; }
        [StringLength(255)]
        public string Marks { get; set; }
        [StringLength(255)]
        public string Grade { get; set; }
        [StringLength(255)]
        public string Color { get; set; }
    }
}
