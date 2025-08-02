namespace  Eduegate.Domain.Entity.School.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Attendences", Schema = "payroll")]
    public partial class Attendence
    {
        [Key]
        public long AttendenceIID { get; set; }

        public int? CompanyID { get; set; }

        public DateTime? AttendenceDate { get; set; }

        public long? EmployeeID { get; set; }

        public bool? IsHalfDay { get; set; }

        public byte? AttendenceStatusID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Company Company { get; set; }

        public virtual AttendenceStatus AttendenceStatus { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
