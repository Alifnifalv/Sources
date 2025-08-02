using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CompanyStatuses", Schema = "mutual")]
    public partial class CompanyStatus
    {
        public CompanyStatus()
        {
            Companies = new HashSet<Company>();
        }

        [Key]
        public byte CompanyStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Company> Companies { get; set; }
    }
}
