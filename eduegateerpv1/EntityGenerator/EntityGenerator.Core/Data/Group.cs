using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Groups", Schema = "account")]
    public partial class Group
    {
        public Group()
        {
            Accounts = new HashSet<Account>();
            BudgetEntryAccountMaps = new HashSet<BudgetEntryAccountMap>();
        }

        [Key]
        public int GroupID { get; set; }
        [StringLength(50)]
        public string GroupCode { get; set; }
        [StringLength(100)]
        public string GroupName { get; set; }
        public int? Parent_ID { get; set; }
        public int Affect_ID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Default_Side { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public bool? IsSystemDefined { get; set; }
        public bool? AllowUserDelete { get; set; }
        public bool? AllowUserEdit { get; set; }
        public bool? AllowAddSubGroup { get; set; }
        public bool? AllowUserRename { get; set; }
        public bool? IsHidden { get; set; }

        [InverseProperty("Group")]
        public virtual ICollection<Account> Accounts { get; set; }
        [InverseProperty("Group")]
        public virtual ICollection<BudgetEntryAccountMap> BudgetEntryAccountMaps { get; set; }
    }
}
