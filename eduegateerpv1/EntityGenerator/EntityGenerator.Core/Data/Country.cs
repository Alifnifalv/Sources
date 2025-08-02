using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Countries", Schema = "mutual")]
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            Companies = new HashSet<Company>();
            DeliveryChargeFromCountries = new HashSet<DeliveryCharge>();
            DeliveryChargeToCountries = new HashSet<DeliveryCharge>();
            DeliveryTypeAllowedCountryMapFromCountries = new HashSet<DeliveryTypeAllowedCountryMap>();
            DeliveryTypeAllowedCountryMapToCountries = new HashSet<DeliveryTypeAllowedCountryMap>();
            EmployeeRelationsDetails = new HashSet<EmployeeRelationsDetail>();
            ParentCountries = new HashSet<Parent>();
            ParentFatherPassportCountryofIssues = new HashSet<Parent>();
            ParentGuardianCountryofIssues = new HashSet<Parent>();
            ParentMotherPassportCountryofIssues = new HashSet<Parent>();
            PassportDetailMaps = new HashSet<PassportDetailMap>();
            PassportVisaDetails = new HashSet<PassportVisaDetail>();
            Routes = new HashSet<Route>();
            ServiceProviders = new HashSet<ServiceProvider>();
            SiteCountryMaps = new HashSet<SiteCountryMap>();
            Streets = new HashSet<Street>();
            StudentApplicationCountries = new HashSet<StudentApplication>();
            StudentApplicationStudentCoutryOfBriths = new HashSet<StudentApplication>();
            StudentCurrentCountries = new HashSet<Student>();
            StudentPassportDetailCountryofBirths = new HashSet<StudentPassportDetail>();
            StudentPassportDetailCountryofIssues = new HashSet<StudentPassportDetail>();
            StudentPermenentCountries = new HashSet<Student>();
            Suppliers = new HashSet<Supplier>();
            Vehicles = new HashSet<Vehicle>();
            Zones = new HashSet<Zone>();
            CountryGroups = new HashSet<ServiceProviderCountryGroup>();
        }

        [Key]
        public int CountryID { get; set; }
        [StringLength(50)]
        public string CountryName { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string TwoLetterCode { get; set; }
        [StringLength(3)]
        [Unicode(false)]
        public string ThreeLetterCode { get; set; }
        public int? CurrencyID { get; set; }
        public int? LanguageID { get; set; }
        [StringLength(20)]
        public string CountryCode { get; set; }

        [ForeignKey("CurrencyID")]
        [InverseProperty("Countries")]
        public virtual Currency Currency { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<City> Cities { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<Company> Companies { get; set; }
        [InverseProperty("FromCountry")]
        public virtual ICollection<DeliveryCharge> DeliveryChargeFromCountries { get; set; }
        [InverseProperty("ToCountry")]
        public virtual ICollection<DeliveryCharge> DeliveryChargeToCountries { get; set; }
        [InverseProperty("FromCountry")]
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMapFromCountries { get; set; }
        [InverseProperty("ToCountry")]
        public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMapToCountries { get; set; }
        [InverseProperty("CountryofIssue")]
        public virtual ICollection<EmployeeRelationsDetail> EmployeeRelationsDetails { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<Parent> ParentCountries { get; set; }
        [InverseProperty("FatherPassportCountryofIssue")]
        public virtual ICollection<Parent> ParentFatherPassportCountryofIssues { get; set; }
        [InverseProperty("GuardianCountryofIssue")]
        public virtual ICollection<Parent> ParentGuardianCountryofIssues { get; set; }
        [InverseProperty("MotherPassportCountryofIssue")]
        public virtual ICollection<Parent> ParentMotherPassportCountryofIssues { get; set; }
        [InverseProperty("CountryofIssue")]
        public virtual ICollection<PassportDetailMap> PassportDetailMaps { get; set; }
        [InverseProperty("CountryofIssue")]
        public virtual ICollection<PassportVisaDetail> PassportVisaDetails { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<Route> Routes { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<Street> Streets { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<StudentApplication> StudentApplicationCountries { get; set; }
        [InverseProperty("StudentCoutryOfBrith")]
        public virtual ICollection<StudentApplication> StudentApplicationStudentCoutryOfBriths { get; set; }
        [InverseProperty("CurrentCountry")]
        public virtual ICollection<Student> StudentCurrentCountries { get; set; }
        [InverseProperty("CountryofBirth")]
        public virtual ICollection<StudentPassportDetail> StudentPassportDetailCountryofBirths { get; set; }
        [InverseProperty("CountryofIssue")]
        public virtual ICollection<StudentPassportDetail> StudentPassportDetailCountryofIssues { get; set; }
        [InverseProperty("PermenentCountry")]
        public virtual ICollection<Student> StudentPermenentCountries { get; set; }
        [InverseProperty("TaxJurisdictionCountry")]
        public virtual ICollection<Supplier> Suppliers { get; set; }
        [InverseProperty("RigistrationCountry")]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        [InverseProperty("Country")]
        public virtual ICollection<Zone> Zones { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("Countries")]
        public virtual ICollection<ServiceProviderCountryGroup> CountryGroups { get; set; }
    }
}
