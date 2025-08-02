using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.HR
{
    [Table("SalaryMethod", Schema = "payroll")]
    public class SalaryMethod
    {
        public SalaryMethod()
        {
            this.Employees = new List<Employee>();
            this.PayrollInfoes = new List<PayrollInfo>();
        }

        [Key]
        public int SalaryMethodID { get; set; }
        public string SalaryMethodName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<PayrollInfo> PayrollInfoes { get; set; }
    }
}
