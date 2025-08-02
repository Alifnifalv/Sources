using Eduegate.Domain.Entity.Models.HR;
using EntityGenerator.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Countries", Schema = "mutual")]
    public partial class Country
    {
        public Country()
        {
            this.SiteCountryMaps = new List<SiteCountryMap>();
            this.ServiceProviders = new List<ServiceProvider>();
            this.DeliveryChargeFromCountries = new List<DeliveryCharge>();
            this.DeliveryChargeToCountries = new List<DeliveryCharge>();
            this.DeliveryTypeAllowedCountryMaps = new List<DeliveryTypeAllowedCountryMap>();
            this.DeliveryTypeAllowedCountryMaps1 = new List<DeliveryTypeAllowedCountryMap>();
            this.DeliveryTypeAllowedZoneMaps = new List<DeliveryTypeAllowedZoneMap>();
            this.Companies = new List<Company>();
            this.Languages = new List<Language>();
            this.Zones = new List<Zone>();
            //Employees = new HashSet<Employee>();
            PassportVisaDetails = new HashSet<PassportVisaDetail>();
            Parents = new HashSet<Parent>();
            Parents1 = new HashSet<Parent>();
            Parents2 = new HashSet<Parent>();
            Suppliers = new HashSet<Supplier>();
            AvailableJobs = new HashSet<AvailableJob>();
            JobSeekers = new HashSet<JobSeeker>();
            JobApplications = new HashSet<JobApplication>();
            JobSeekerPassportIssueCountries = new HashSet<JobSeeker>();
        }

        [Key]
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public virtual ICollection<Area> Area { get; set; }
        public virtual ICollection<Route> Route { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
        public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryChargeFromCountries { get; set; }
        public virtual ICollection<DeliveryCharge> DeliveryChargeToCountries { get; set; }
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps1 { get; set; }
        public virtual ICollection<DeliveryTypeAllowedZoneMap> DeliveryTypeAllowedZoneMaps { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Zone> Zones { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Employee> Employees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PassportVisaDetail> PassportVisaDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parent> Parents { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parent> Parents1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Parent> Parents2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier> Suppliers { get; set; }

        public virtual ICollection<AvailableJob> AvailableJobs { get; set; }
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
        public virtual ICollection<JobApplication> JobApplications { get; set; }
        public virtual ICollection<JobSeeker> JobSeekerPassportIssueCountries { get; set; }

    }
}
