using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.HR.Models
{
    [Table("Countries", Schema = "mutual")]
    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            Logins = new HashSet<Login>();
            Airports = new HashSet<Airport>();
            //SiteCountryMaps = new HashSet<SiteCountryMap>();
            //Routes = new HashSet<Route>();
            //ServiceProviders = new HashSet<ServiceProvider>();
            //DeliveryCharges = new HashSet<DeliveryCharge>();
            //DeliveryCharges1 = new HashSet<DeliveryCharge>();
            //DeliveryTypeAllowedCountryMaps = new HashSet<DeliveryTypeAllowedCountryMap>();
            //DeliveryTypeAllowedCountryMaps1 = new HashSet<DeliveryTypeAllowedCountryMap>();
            //Cities = new HashSet<City>();
            Companies = new HashSet<Company>();
            //Parents = new HashSet<Parent>();
            //Parents1 = new HashSet<Parent>();
            //Parents2 = new HashSet<Parent>();
            //PassportDetailMaps = new HashSet<PassportDetailMap>();
            PassportVisaDetails = new HashSet<PassportVisaDetail>();
            //StudentApplications = new HashSet<StudentApplication>();
            //StudentApplications1 = new HashSet<StudentApplication>();
            //StudentPassportDetails = new HashSet<StudentPassportDetail>();
            //StudentPassportDetails1 = new HashSet<StudentPassportDetail>();
            //Students = new HashSet<Student>();
            //Students1 = new HashSet<Student>();
            //Vehicles = new HashSet<Vehicle>();
            //Zones = new HashSet<Zone>();
            //ServiceProviderCountryGroups = new HashSet<ServiceProviderCountryGroup>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryID { get; set; }

        [StringLength(50)]
        public string CountryName { get; set; }

        [StringLength(2)]
        public string TwoLetterCode { get; set; }

        [StringLength(3)]
        public string ThreeLetterCode { get; set; }

        public int? CurrencyID { get; set; }

        public int? LanguageID { get; set; }

        [StringLength(20)]
        public string CountryCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login> Logins { get; set; }


        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SiteCountryMap> SiteCountryMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Route> Routes { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DeliveryCharge> DeliveryCharges1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<DeliveryTypeAllowedCountryMap> DeliveryTypeAllowedCountryMaps1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<City> Cities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Company> Companies { get; set; }

        //public virtual Currency Currency { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Parent> Parents { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Parent> Parents1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Parent> Parents2 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PassportDetailMap> PassportDetailMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PassportVisaDetail> PassportVisaDetails { get; set; }
        public virtual ICollection<Airport> Airports { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentApplication> StudentApplications { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentApplication> StudentApplications1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentPassportDetail> StudentPassportDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<StudentPassportDetail> StudentPassportDetails1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Student> Students1 { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Vehicle> Vehicles { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Zone> Zones { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ServiceProviderCountryGroup> ServiceProviderCountryGroups { get; set; }
    }
}