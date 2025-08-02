using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VehicleDetailMaps", Schema = "mutual")]
    public partial class VehicleDetailMap
    {
        [Key]
        public long VehicleDetailMapIID { get; set; }
        public long? VehicleID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationExpiryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsuranceIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsuranceExpiryDate { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(30)]
        public string InsuranceNumber { get; set; }
        [StringLength(50)]
        public string InsuranceCompany { get; set; }
        [StringLength(20)]
        public string VehicleNamePlate { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("VehicleDetailMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("VehicleDetailMaps")]
        public virtual School School { get; set; }
        [ForeignKey("VehicleID")]
        [InverseProperty("VehicleDetailMaps")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
