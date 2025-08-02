using Eduegate.Domain.Entity.HR.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Payroll
{
    [Table("EmployeeSalaries", Schema = "payroll")]
    public partial class EmployeeSalary
    {
        [Key]
        public long EmployeeSalaryIID { get; set; }

        public int? CompanyID { get; set; }

        public long? EmployeeID { get; set; }

        public int? SalaryComponentID { get; set; }

        public decimal? Amount { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public virtual Company Company { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual SalaryComponent SalaryComponent { get; set; }
    }
}