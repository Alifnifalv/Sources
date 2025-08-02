using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TransportApplctnStudentMaps", Schema = "schools")]
    [Index("SchoolID", Name = "IDX_TransportApplctnStudentMaps_SchoolID_TransportApplicationID__StudentID__StartDate__IsActive__Is")]
    [Index("SchoolID", Name = "IDX_TransportApplctnStudentMaps_SchoolID_TransportApplicationID__StudentID__TransportApplcnStatusID")]
    [Index("SchoolID", "TransportApplcnStatusID", Name = "IDX_TransportApplctnStudentMaps_SchoolID__TransportApplcnStatusID_TransportApplicationID__StudentID")]
    [Index("StudentID", Name = "IDX_TransportApplctnStudentMaps_StudentID_")]
    [Index("StudentID", "SchoolID", Name = "IDX_TransportApplctnStudentMaps_StudentID__SchoolID_TransportApplicationID__StartDate__IsActive__Is")]
    [Index("StudentID", "SchoolID", Name = "IDX_TransportApplctnStudentMaps_StudentID__SchoolID_TransportApplicationID__TransportApplcnStatusID")]
    [Index("TransportApplicationID", Name = "IDX_TransportApplctnStudentMaps_TransportApplicationID_")]
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
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsNewRider { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public byte? TransportApplcnStatusID { get; set; }
        public bool? IsMedicalCondition { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public int? AcademicYearID { get; set; }
        public string Remarks1 { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("TransportApplctnStudentMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        [InverseProperty("TransportApplctnStudentMaps")]
        public virtual Class Class { get; set; }
        [ForeignKey("GenderID")]
        [InverseProperty("TransportApplctnStudentMaps")]
        public virtual Gender Gender { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("TransportApplctnStudentMaps")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("TransportApplctnStudentMaps")]
        public virtual Student Student { get; set; }
        [ForeignKey("TransportApplcnStatusID")]
        [InverseProperty("TransportApplctnStudentMaps")]
        public virtual TransportApplicationStatus TransportApplcnStatus { get; set; }
        [ForeignKey("TransportApplicationID")]
        [InverseProperty("TransportApplctnStudentMaps")]
        public virtual TransportApplication TransportApplication { get; set; }
    }
}
