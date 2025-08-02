using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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

        [InverseProperty("NewLeaveGroup")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionNewLeaveGroups { get; set; }
        [InverseProperty("OldLeaveGroup")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldLeaveGroups { get; set; }
        [InverseProperty("LeaveGroup")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
