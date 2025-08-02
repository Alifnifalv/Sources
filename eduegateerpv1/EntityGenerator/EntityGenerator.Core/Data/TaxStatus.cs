using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TaxStatuses", Schema = "inventory")]
    public partial class TaxStatus
    {
        public TaxStatus()
        {
            Taxes = new HashSet<Tax>();
        }

        [Key]
        public int TaxStatusID { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        [InverseProperty("TaxStatus")]
        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
