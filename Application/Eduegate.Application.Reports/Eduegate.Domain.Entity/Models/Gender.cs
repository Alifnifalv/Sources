using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Gender
    {
        public Gender()
        {
            this.Employees = new List<Employee>();
        }

        public byte GenderID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
