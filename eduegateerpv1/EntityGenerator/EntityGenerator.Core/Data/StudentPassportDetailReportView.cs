using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentPassportDetailReportView
    {
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long StudentIID { get; set; }
        [StringLength(502)]
        public string StudentName { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(20)]
        public string PassportNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportNoExpiry { get; set; }
        [StringLength(20)]
        public string VisaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaExpiry { get; set; }
        [StringLength(20)]
        public string NationalIDNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NationalIDNoExpiry { get; set; }
        public int ClassID { get; set; }
        public bool? IsActive { get; set; }
        public byte? SchoolID { get; set; }
    }
}
