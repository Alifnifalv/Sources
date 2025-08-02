using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.HR.Models.Payroll
{
    [Table("LSProvisions", Schema = "payroll")]
    public class LSProvision
    {
        public DateTime? SlipDate { get; set; }

        public long? EmployeeID { get; set; }
    }
}
