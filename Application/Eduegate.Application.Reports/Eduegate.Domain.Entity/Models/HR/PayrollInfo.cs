using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.HR
{
    public class PayrollInfo
    {
        [Key, ForeignKey("Employee")]
        public long EmployeeID { get; set; }
        public int SalaryMethodID { get; set; }
        public decimal BasicSalary { get; set; }
        public string OtherBenefits { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual SalaryMethod SalaryMethod { get; set; }
    }
}
