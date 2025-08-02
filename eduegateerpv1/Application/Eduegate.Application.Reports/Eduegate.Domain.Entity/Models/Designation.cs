using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Designation
    {
        public Designation()
        {
            this.Employees = new List<Employee>();
        }

        public int DesignationID { get; set; }

        public string DesignationName { get; set; }

        public string DesignationCode { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}