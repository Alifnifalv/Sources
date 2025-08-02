using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("FacultyTypes", Schema = "schools")]
    public partial class FacultyType
    {
        public FacultyType()
        {
            Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int FacultyTypeID { get; set; }
        [StringLength(100)]
        public string FacultyTypeName { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("FacultyType")]
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
