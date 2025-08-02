using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SalaryMethod
    {
        public SalaryMethod()
        {
            this.Employees = new List<Employee>();
        }

        public int SalaryMethodID { get; set; }
        public string SalaryMethodName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
