using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ServiceProviders", Schema = "distribution")]
    public partial class ServiceProvider
    {
        public ServiceProvider()
        {
            DeliveryCharges = new HashSet<DeliveryCharge>();
            ServiceProviderCountryGroups = new HashSet<ServiceProviderCountryGroup>();
            ServiceProviderLogs = new HashSet<ServiceProviderLog>();
        }

        [Key]
        public int ServiceProviderID { get; set; }
        [StringLength(20)]
        public string ProviderCode { get; set; }
        [StringLength(50)]
        public string ProviderName { get; set; }
        public int? CountryID { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        [StringLength(500)]
        public string ServiceProviderLink { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("ServiceProviders")]
        public virtual Country Country { get; set; }
        [InverseProperty("ServiceProvider")]
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }
        [InverseProperty("ServiceProvider")]
        public virtual ICollection<ServiceProviderCountryGroup> ServiceProviderCountryGroups { get; set; }
        [InverseProperty("ServiceProvider")]
        public virtual ICollection<ServiceProviderLog> ServiceProviderLogs { get; set; }
    }
}
