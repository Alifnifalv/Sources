using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ChartOfAccounts", Schema = "account")]
    public partial class ChartOfAccount
    {
        public ChartOfAccount()
        {
            ChartOfAccountMaps = new HashSet<ChartOfAccountMap>();
        }

        [Key]
        public long ChartOfAccountIID { get; set; }
        [StringLength(100)]
        public string ChartName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [InverseProperty("ChartOfAccount")]
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
    }
}
