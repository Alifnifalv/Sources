using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceProviderCountryGroup
    {
        public ServiceProviderCountryGroup()
        {
            this.DeliveryCharges = new List<DeliveryCharge>();
            this.Countries = new List<Country>();
        }

        public long CountryGroupID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ServiceProviderID { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
    }
}
