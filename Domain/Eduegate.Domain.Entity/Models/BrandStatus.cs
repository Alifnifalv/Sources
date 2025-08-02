using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("BrandStatuses", Schema = "catalog")]
    public partial class BrandStatus
    {
        public BrandStatus()
        {
            this.Brands = new List<Brand>();
        }

        public byte BrandStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
