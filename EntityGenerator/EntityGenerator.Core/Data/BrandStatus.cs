using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BrandStatuses", Schema = "catalog")]
    public partial class BrandStatus
    {
        public BrandStatus()
        {
            Brands = new HashSet<Brand>();
        }

        [Key]
        public byte BrandStatusID { get; set; }
        [StringLength(100)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
