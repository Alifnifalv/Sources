using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Designations", Schema = "payroll")]
    public partial class Designation
    {
        public Designation()
        {
            this.Employees = new List<Employee>();
            AvailableJobs = new HashSet<AvailableJob>();
        }

        [Key]
        public int DesignationID { get; set; }

        public string DesignationName { get; set; }

        public string DesignationCode { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }
    }
}