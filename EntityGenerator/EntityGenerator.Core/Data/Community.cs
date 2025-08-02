using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Communitys", Schema = "schools")]
    public partial class Community
    {
        public Community()
        {
            Employees = new HashSet<Employee>();
            Students = new HashSet<Student>();
        }

        [Key]
        public byte CommunityID { get; set; }
        [StringLength(50)]
        public string CommunityDescription { get; set; }

        [InverseProperty("Community")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Community")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
