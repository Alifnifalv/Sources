using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class OnlineAppReportview
    {
        public long ApplicationIID { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        [StringLength(15)]
        public string PreviousSchoolAcademicYear { get; set; }
        [StringLength(62)]
        public string StudentName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string MobileNumber { get; set; }
        [StringLength(20)]
        public string EmailID { get; set; }
        [StringLength(50)]
        public string ParmenentAddress { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [StringLength(62)]
        public string ParentName { get; set; }
        [StringLength(20)]
        public string FatherOccupation { get; set; }
        [StringLength(25)]
        public string PreviousSchoolName { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string RelegionName { get; set; }
        [StringLength(50)]
        public string ApplicationStatus { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(100)]
        public string AcademicYear { get; set; }
        [StringLength(50)]
        public string Expr6 { get; set; }
        [StringLength(50)]
        public string SyllabusDescription { get; set; }
        public int ClassID { get; set; }
    }
}
