using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssignmentTypes", Schema = "schools")]
    public partial class AssignmentType
    {
        public AssignmentType()
        {
            Assignments = new HashSet<Assignment>();
        }

        [Key]
        public byte AssignmentTypeID { get; set; }
        [StringLength(50)]
        public string TypeName { get; set; }

        [InverseProperty("AssignmentType")]
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
