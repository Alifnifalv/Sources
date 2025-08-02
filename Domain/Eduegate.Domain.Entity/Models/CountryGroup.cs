using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CountryGroup
    {
        public CountryGroup()
        {
            this.CountryMasters = new List<CountryMaster>();
        }

        [Key]
        public int GroupID { get; set; }
        public string GroupType { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> MinWeight { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public Nullable<int> AfterMinWeight { get; set; }
        public Nullable<decimal> AfterMinAmount { get; set; }
        public virtual ICollection<CountryMaster> CountryMasters { get; set; }
    }
}
