using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Countries1
    {
        public Countries1()
        {
            this.ServiceProviders = new List<ServiceProvider>();
            this.DeliveryCharges = new List<DeliveryCharge>();
            this.DeliveryCharges1 = new List<DeliveryCharge>();
            this.DeliveryTypeAllowedCountryMaps = new List<DeliveryTypeAllowedCountryMap>();
            this.DeliveryTypeAllowedCountryMaps1 = new List<DeliveryTypeAllowedCountryMap>();
            this.DeliveryTypeAllowedZoneMaps = new List<DeliveryTypeAllowedZoneMap>();
            this.Companies = new List<Company>();
        }

        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges1 { get; set; }
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps1 { get; set; }
        public virtual ICollection<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Language1 Language { get; set; }
    }
}
