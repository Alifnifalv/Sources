using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("TransportApplctnStudentMaps", Schema = "schools")]
    public partial class TransportApplctnStudentMap
    {
        [Key]
        public long TransportApplctnStudentMapIID { get; set; }

        public long? TransportApplicationID { get; set; }

        public int? ClassID { get; set; }

        public long? StudentID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public byte? GenderID { get; set; }

        public DateTime? StartDate { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsNewRider { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public byte? TransportApplcnStatusID { get; set; }

        public bool? IsMedicalCondition { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }

        public int? AcademicYearID { get; set; }

        public string Remarks1 { get; set; }

        public virtual AcademicYear AcademicYears { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Class Class { get; set; }

        public virtual Schools School { get; set; }

        public virtual Student Student { get; set; }

        public virtual TransportApplicationStatus TransportApplicationStatus { get; set; }

        public virtual TransportApplication TransportApplication { get; set; }
    }
}