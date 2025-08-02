using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ChartRowTypes", Schema = "account")]
    public partial class ChartRowType
    {
        public ChartRowType()
        {
            this.ChartOfAccountMaps = new List<ChartOfAccountMap>();
        }

        [Key]
        public int ChartRowTypeID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ChartOfAccountMap> ChartOfAccountMaps { get; set; }
    }
}
