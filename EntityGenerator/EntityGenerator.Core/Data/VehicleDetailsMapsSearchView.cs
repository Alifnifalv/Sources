using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VehicleDetailsMapsSearchView
    {
        public long VehicleDetailMapIID { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        public long? VehicleID { get; set; }
        [StringLength(50)]
        public string Vehicle { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string RegistrationDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string RegistrationExpiryDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string InsuranceIssueDate { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string InsuranceExpiryDate { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
