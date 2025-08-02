using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BranchStatuses", Schema = "mutual")]
    public partial class BranchStatus
    {
        public BranchStatus()
        {
            Branches = new HashSet<Branch>();
        }

        [Key]
        public byte BranchStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
