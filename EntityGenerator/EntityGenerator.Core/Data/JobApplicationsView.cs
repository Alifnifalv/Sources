using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class JobApplicationsView
    {
        public long ApplicationIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AppliedDate { get; set; }
        [StringLength(100)]
        public string JobAppliedFor { get; set; }
        [StringLength(250)]
        public string ReferenceCode { get; set; }
        public int? CountryOfResidenceID { get; set; }
        [Required]
        public string ApplicantName { get; set; }
        public int? Status { get; set; }
        public byte? SchoolID { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        public string CreatedUserName { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
