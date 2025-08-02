using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Payrolls
{
    [Table("LeaveGroups", Schema = "payroll")]
    public partial class LeaveGroup
    {
        public LeaveGroup()
        {
            EmployeePromotionNewLeaveGroups = new HashSet<EmployeePromotion>();
            EmployeePromotionOldLeaveGroups = new HashSet<EmployeePromotion>();
        }

        [Key]
        public int LeaveGroupID { get; set; }

        [StringLength(50)]
        public string LeaveGroupName { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotionNewLeaveGroups { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotionOldLeaveGroups { get; set; }
    }
}