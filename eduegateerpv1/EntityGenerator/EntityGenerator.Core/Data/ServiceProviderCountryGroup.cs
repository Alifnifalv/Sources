using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ServiceProviderCountryGroups", Schema = "distribution")]
    public partial class ServiceProviderCountryGroup
    {
        public ServiceProviderCountryGroup()
        {
            DeliveryCharges = new HashSet<DeliveryCharge>();
            Countries = new HashSet<Country>();
        }

        [Key]
        public long CountryGroupID { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string Name { get; set; }
        public int? ServiceProviderID { get; set; }

        [ForeignKey("ServiceProviderID")]
        [InverseProperty("ServiceProviderCountryGroups")]
        public virtual ServiceProvider ServiceProvider { get; set; }
        [InverseProperty("CountryGroup")]
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }

        [ForeignKey("CountryGroupID")]
        [InverseProperty("CountryGroups")]
        public virtual ICollection<Country> Countries { get; set; }
    }
}
