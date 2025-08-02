using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("PricingTypes", Schema = "saloon")]
    public partial class PricingType
    {
        public PricingType()
        {
            Services = new HashSet<Service>();
        }

        [Key]
        public int PricingTypeID { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        [InverseProperty("PricingType")]
        public virtual ICollection<Service> Services { get; set; }
    }
}
