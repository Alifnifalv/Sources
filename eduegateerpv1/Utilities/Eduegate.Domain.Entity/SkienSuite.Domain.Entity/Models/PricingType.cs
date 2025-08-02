using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PricingType
    {
        public PricingType()
        {
            this.Services = new List<Service>();
        }

        public int PricingTypeID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
