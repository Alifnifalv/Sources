using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Relegions", Schema = "mutual")]
    public partial class Relegion
    {
        public Relegion()
        {
            Casts = new HashSet<Cast>();
            Employees = new HashSet<Employee>();
            StudentApplications = new HashSet<StudentApplication>();
            Students = new HashSet<Student>();
        }

        [Key]
        public byte RelegionID { get; set; }
        [StringLength(50)]
        public string RelegionName { get; set; }
        [StringLength(20)]
        public string RelegionCode { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Relegion")]
        public virtual ICollection<Cast> Casts { get; set; }
        [InverseProperty("Relegion")]
        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty("Relegion")]
        public virtual ICollection<StudentApplication> StudentApplications { get; set; }
        [InverseProperty("Relegion")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
