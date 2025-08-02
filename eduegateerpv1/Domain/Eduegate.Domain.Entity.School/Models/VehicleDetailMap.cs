namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("VehicleDetailMaps", Schema = "mutual")]
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

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
