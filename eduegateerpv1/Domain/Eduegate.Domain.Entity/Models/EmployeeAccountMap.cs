using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EmployeeAccountMaps", Schema = "payroll")]
    public partial class EmployeeAccountMap
    {
        [Key]
        public long EmployeeAccountMapIID { get; set; }
        public Nullable<long> EmployeeID { get; set; }
        public Nullable<long> AccountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
