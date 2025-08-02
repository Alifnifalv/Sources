using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryStudentRegisters", Schema = "schools")]
    public partial class LibraryStudentRegister
    {
        [Key]
        public long LibraryStudentRegisterIID { get; set; }
        [StringLength(50)]
        public string LibraryCardNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegistrationDate { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("LibraryStudentRegisters")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("LibraryStudentRegisters")]
        public virtual School School { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("LibraryStudentRegisters")]
        public virtual Student Student { get; set; }
    }
}
