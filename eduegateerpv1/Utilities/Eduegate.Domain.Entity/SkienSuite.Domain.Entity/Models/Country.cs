using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Country
    {
        public Country()
        {
            this.Logins = new List<Login>();
            this.SiteCountryMaps = new List<SiteCountryMap>();
            this.Routes = new List<Route>();
            this.ServiceProviders = new List<ServiceProvider>();
            this.DeliveryCharges = new List<DeliveryCharge>();
            this.DeliveryCharges1 = new List<DeliveryCharge>();
            this.DeliveryTypeAllowedCountryMaps = new List<DeliveryTypeAllowedCountryMap>();
            this.DeliveryTypeAllowedCountryMaps1 = new List<DeliveryTypeAllowedCountryMap>();
            this.Areas = new List<Area>();
            this.Cities = new List<City>();
            this.Companies = new List<Company>();
            this.Languages = new List<Language>();
            this.Vehicles = new List<Vehicle>();
            this.Zones = new List<Zone>();
            this.ServiceProviderCountryGroups = new List<ServiceProviderCountryGroup>();
        }

        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryCharges1 { get; set; }
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps1 { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual Language Language { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }
        public virtual ICollection<ServiceProviderCountryGroup> ServiceProviderCountryGroups { get; set; }
    }
}
