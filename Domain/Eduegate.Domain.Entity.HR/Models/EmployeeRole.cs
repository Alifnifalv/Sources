namespace Eduegate.Domain.Entity.HR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeRoles", Schema = "payroll")]
    public partial class EmployeeRole
    {
        public EmployeeRole()
        {
            EmployeePromotionNewRoles = new HashSet<EmployeePromotion>();
            EmployeePromotionOldRoles = new HashSet<EmployeePromotion>();
            EmployeeRoleMaps = new HashSet<EmployeeRoleMap>();
            Employees = new HashSet<Employee>();
        }

        [Key]
        public int EmployeeRoleID { get; set; }
        [StringLength(50)]
        public string EmployeeRoleName { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionNewRoles { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldRoles { get; set; }
        public virtual ICollection<EmployeeRoleMap> EmployeeRoleMaps { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
