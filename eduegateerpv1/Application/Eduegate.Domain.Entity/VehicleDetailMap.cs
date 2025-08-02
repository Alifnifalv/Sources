namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.VehicleDetailMaps")]
    public partial class VehicleDetailMap
    {
        [Key]
        public long VehicleDetailMapIID { get; set; }

        public long? VehicleID { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? RegistrationExpiryDate { get; set; }

        public DateTime? InsuranceIssueDate { get; set; }

        public DateTime? InsuranceExpiryDate { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(30)]
        public string InsuranceNumber { get; set; }

        [StringLength(50)]
        public string InsuranceCompany { get; set; }

        [StringLength(20)]
        public string VehicleNamePlate { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
