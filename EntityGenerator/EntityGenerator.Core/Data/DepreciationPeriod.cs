using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("DepreciationPeriods", Schema = "asset")]
    public partial class DepreciationPeriod
    {
        public DepreciationPeriod()
        {
            AssetCategories = new HashSet<AssetCategory>();
        }

        [Key]
        public int PeriodID { get; set; }
        [StringLength(100)]
        public string PeriodName { get; set; }

        [InverseProperty("DepreciationPeriod")]
        public virtual ICollection<AssetCategory> AssetCategories { get; set; }
    }
}
