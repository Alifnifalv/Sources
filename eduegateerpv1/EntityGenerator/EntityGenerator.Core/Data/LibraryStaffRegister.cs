using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("LibraryStaffRegisters", Schema = "schools")]
    public partial class LibraryStaffRegister
    {
        [Key]
        public long LibraryStaffResiterIID { get; set; }
        public long? EmployeeID { get; set; }
        [StringLength(50)]
        public string LibraryCardNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime RegistrationDate { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("LibraryStaffRegisters")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("LibraryStaffRegisters")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("LibraryStaffRegisters")]
        public virtual School School { get; set; }
    }
}
