using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Designations", Schema = "payroll")]
    public partial class Designation
    {
        public Designation()
        {
            AvailableJobs = new HashSet<AvailableJob>();
            EmployeePromotionNewDesignations = new HashSet<EmployeePromotion>();
            EmployeePromotionOldDesignations = new HashSet<EmployeePromotion>();
            Employees = new HashSet<Employee>();
            JobDescriptions = new HashSet<JobDescription>();
            LookupColumnConditionMaps = new HashSet<LookupColumnConditionMap>();
        }

        [Key]
        public int DesignationID { get; set; }
        [StringLength(50)]
        public string DesignationName { get; set; }
        [StringLength(50)]
        public string DesignationCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Required]
        public byte[] TimeStamps { get; set; }
        public bool? IsTransportNotification { get; set; }

        [InverseProperty("Designation")]
        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }
        [InverseProperty("NewDesignation")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionNewDesignations { get; set; }
        [InverseProperty("OldDesignation")]
        public virtual ICollection<EmployeePromotion> EmployeePromotionOldDesignations { get; set; }
        [InverseProperty("Designation")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Designation")]
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
        [InverseProperty("Designation")]
        public virtual ICollection<LookupColumnConditionMap> LookupColumnConditionMaps { get; set; }
    }
}
