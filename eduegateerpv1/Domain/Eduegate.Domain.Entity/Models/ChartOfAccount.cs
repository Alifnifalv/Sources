using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ChartOfAccounts", Schema = "account")]
    public partial class ChartOfAccount
    {
        public ChartOfAccount()
        {
            this.ChartOfAccountMaps = new List<ChartOfAccountMap>();
        }

        [Key]
        public long ChartOfAccountIID { get; set; }
        public string ChartName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
    }
}
