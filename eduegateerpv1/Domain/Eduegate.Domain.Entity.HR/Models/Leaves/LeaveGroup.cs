using Eduegate.Domain.Entity.HR.Leaves;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.HR.Models.Leaves
{
    [Table("LeaveGroups", Schema = "payroll")]
    public partial class LeaveGroup
    {
        public LeaveGroup()
        {
            EmployeePromotionNewLeaveGroups = new HashSet<EmployeePromotion>();
            EmployeePromotionOldLeaveGroups = new HashSet<EmployeePromotion>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int LeaveGroupID { get; set; }
        [StringLength(50)]
        public string LeaveGroupName { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionNewLeaveGroups { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldLeaveGroups { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }

}
