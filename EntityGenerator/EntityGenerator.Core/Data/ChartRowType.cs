using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ChartRowTypes", Schema = "account")]
    public partial class ChartRowType
    {
        public ChartRowType()
        {
            ChartOfAccountMaps = new HashSet<ChartOfAccountMap>();
        }

        [Key]
        public int ChartRowTypeID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("ChartRowType")]
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
    }
}
