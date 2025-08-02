using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CompanyGroups", Schema = "mutual")]
    public partial class CompanyGroup
    {
        public CompanyGroup()
        {
            Companies = new HashSet<Company>();
        }

        [Key]
        public int CompanyGroupID { get; set; }
        [StringLength(50)]
        public string GroupName { get; set; }

        [InverseProperty("CompanyGroup")]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
