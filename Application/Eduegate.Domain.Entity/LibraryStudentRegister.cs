namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.LibraryStudentRegisters")]
    public partial class LibraryStudentRegister
    {
        [Key]
        public long LibraryStudentRegisterIID { get; set; }

        [StringLength(50)]
        public string LibraryCardNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }

        public long? StudentID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual Student Student { get; set; }
    }
}
