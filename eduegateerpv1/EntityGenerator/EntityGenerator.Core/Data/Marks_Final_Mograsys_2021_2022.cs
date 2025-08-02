using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class Marks_Final_Mograsys_2021_2022
    {
        [Column("Sr No")]
        [StringLength(255)]
        public string Sr_No { get; set; }
        [Column("Academic Year")]
        [StringLength(255)]
        public string Academic_Year { get; set; }
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
        public string SEN { get; set; }
        [Column("G&T")]
        [StringLength(255)]
        public string G_T { get; set; }
        [StringLength(255)]
        public string Arab { get; set; }
        [StringLength(255)]
        public string Emirati { get; set; }
        [StringLength(255)]
        public string Subject { get; set; }
        [StringLength(255)]
        public string Frequency { get; set; }
        [Column("Mark Entry Type")]
        [StringLength(255)]
        public string Mark_Entry_Type { get; set; }
        [StringLength(255)]
        public string Exam { get; set; }
        [StringLength(255)]
        public string OutOf100 { get; set; }
        [StringLength(255)]
        public string Grade { get; set; }
        [StringLength(255)]
        public string OutOfPointPossible { get; set; }
        [Column("Exam Attendance Status")]
        [StringLength(255)]
        public string Exam_Attendance_Status { get; set; }
        [StringLength(255)]
        public string PointPoss { get; set; }
    }
}
