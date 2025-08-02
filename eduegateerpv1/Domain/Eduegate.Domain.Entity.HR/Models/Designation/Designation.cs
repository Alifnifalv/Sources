namespace Eduegate.Domain.Entity.HR.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Designations", Schema = "payroll")]
    public partial class Designation
    {
        public Designation()
        {
            EmployeePromotionNewDesignations = new HashSet<EmployeePromotion>();
            EmployeePromotionOldDesignations = new HashSet<EmployeePromotion>();
            Employees = new HashSet<Employee>();
            //LookupColumnConditionMaps = new HashSet<LookupColumnConditionMap>();
        }

        [Key]
        public int DesignationID { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        [StringLength(50)]
        public string DesignationCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsTransportNotification { get; set; }

        public virtual ICollection<EmployeePromotion> EmployeePromotionNewDesignations { get; set; }
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldDesignations { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        //[InverseProperty("Designation")]
        //public virtual ICollection<LookupColumnConditionMap> LookupColumnConditionMaps { get; set; }
    }
}
