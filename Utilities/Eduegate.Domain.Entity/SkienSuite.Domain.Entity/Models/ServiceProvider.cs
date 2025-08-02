using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceProvider
    {
        public ServiceProvider()
        {
            this.ServiceProviderCountryGroups = new List<ServiceProviderCountryGroup>();
            this.ServiceProviderLogs = new List<ServiceProviderLog>();
            this.DeliveryCharges = new List<DeliveryCharge>();
        }

        public int ServiceProviderID { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string ServiceProviderLink { get; set; }
        public virtual ICollection<ServiceProviderCountryGroup> ServiceProviderCountryGroups { get; set; }
        public virtual ICollection<ServiceProviderLog> ServiceProviderLogs { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual Country Country { get; set; }
    }
}
