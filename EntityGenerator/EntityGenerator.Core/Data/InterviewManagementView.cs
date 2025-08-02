using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class InterviewManagementView
    {
        public long InterviewID { get; set; }
        public long? InterviewerID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(100)]
        public string JobTitle { get; set; }
        [Required]
        [StringLength(555)]
        public string Interviewer { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public int? NoOfApplicants { get; set; }
        public int? NoOfInterest { get; set; }
        public int? NoOfSelected { get; set; }
    }
}
