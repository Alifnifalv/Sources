using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Attendences", Schema = "payroll")]
    public partial class Attendence
    {
        [Key]
        public long AttendenceIID { get; set; }
        public int? CompanyID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttendenceDate { get; set; }
        public long? EmployeeID { get; set; }
        public bool? IsHalfDay { get; set; }
        public byte? AttendenceStatusID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("AttendenceStatusID")]
        [InverseProperty("Attendences")]
        public virtual AttendenceStatus AttendenceStatus { get; set; }
        [ForeignKey("CompanyID")]
        [InverseProperty("Attendences")]
        public virtual Company Company { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("Attendences")]
        public virtual Employee Employee { get; set; }
    }
}
