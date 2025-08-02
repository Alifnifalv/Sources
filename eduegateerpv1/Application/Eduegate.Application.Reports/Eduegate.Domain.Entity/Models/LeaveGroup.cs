namespace Eduegate.Domain.Entity.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("payroll.LeaveGroups")]
    public partial class LeaveGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LeaveGroup()
        {
            Employees = new HashSet<Employee>();
            //LeaveGroupTypeMaps = new HashSet<LeaveGroupTypeMap>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LeaveGroupID { get; set; }

        [StringLength(50)]
        public string LeaveGroupName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LeaveGroupTypeMap> LeaveGroupTypeMaps { get; set; }
    }
}