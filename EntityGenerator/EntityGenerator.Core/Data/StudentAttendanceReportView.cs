using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentAttendanceReportView
    {
        public long StudentIID { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(555)]
        public string StudentFullName { get; set; }
        [StringLength(502)]
        public string StudentSmallName { get; set; }
        [StringLength(126)]
        public string AcademicYear { get; set; }
        public int ClassID { get; set; }
        public int? ClassOrderNumber { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AdmissionDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendanceDate { get; set; }
        [StringLength(500)]
        public string Reason { get; set; }
        public byte? PresentStatusID { get; set; }
        [StringLength(50)]
        public string PresentStatusDescription { get; set; }
        [StringLength(10)]
        public string PresentStatusTitle { get; set; }
    }
}
