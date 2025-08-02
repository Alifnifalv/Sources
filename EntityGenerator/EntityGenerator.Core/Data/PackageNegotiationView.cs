using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class PackageNegotiationView
    {
        public long MapID { get; set; }
        public long? InterviewID { get; set; }
        public long? ApplicantID { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(100)]
        public string JobTitle { get; set; }
        [StringLength(50)]
        public string DepartmentName { get; set; }
        [StringLength(50)]
        public string JobTypeName { get; set; }
        [StringLength(250)]
        public string MonthlySalary { get; set; }
        public long? CreatedBy { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        public byte? SchoolID { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsOfferLetterSent { get; set; }
        [Required]
        [StringLength(3)]
        [Unicode(false)]
        public string IsAgreementSigned { get; set; }
    }
}
