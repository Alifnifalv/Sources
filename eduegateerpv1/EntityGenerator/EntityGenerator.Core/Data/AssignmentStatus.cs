using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssignmentStatuses", Schema = "schools")]
    public partial class AssignmentStatus
    {
        public AssignmentStatus()
        {
            Assignments = new HashSet<Assignment>();
            StudentAssignmentMaps = new HashSet<StudentAssignmentMap>();
        }

        [Key]
        public byte AssignmentStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("AssignmentStatus")]
        public virtual ICollection<Assignment> Assignments { get; set; }
        [InverseProperty("AssignmentStatus")]
        public virtual ICollection<StudentAssignmentMap> StudentAssignmentMaps { get; set; }
    }
}
