using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BranchGroups", Schema = "mutual")]
    public partial class BranchGroup
    {
        public BranchGroup()
        {
            Branches = new HashSet<Branch>();
        }

        [Key]
        public long BranchGroupIID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? StatusID { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("StatusID")]
        [InverseProperty("BranchGroups")]
        public virtual BranchGroupStatus Status { get; set; }
        [InverseProperty("BranchGroup")]
        public virtual ICollection<Branch> Branches { get; set; }
    }
}
