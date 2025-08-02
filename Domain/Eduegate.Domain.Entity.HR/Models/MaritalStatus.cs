using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("MaritalStatuses", Schema = "mutual")]
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            this.Employees = new List<Employee>();
            SalaryStructureScaleMaps = new HashSet<SalaryStructureScaleMap>();
        }

        [Key]
        public int MaritalStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<SalaryStructureScaleMap> SalaryStructureScaleMaps { get; set; }

    }
}
