namespace Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LibraryStaffRegisters", Schema = "schools")]
    public partial class LibraryStaffRegister
    {
        [Key]
        public long LibraryStaffResiterIID { get; set; }

        public long? EmployeeID { get; set; }

        [StringLength(50)]
        public string LibraryCardNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }

        public virtual Employee Employee { get; set; }


        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }
    }
}
