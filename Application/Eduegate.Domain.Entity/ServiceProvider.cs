namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.ServiceProviders")]
    public partial class ServiceProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ServiceProvider()
        {
            ServiceProviderCountryGroups = new HashSet<ServiceProviderCountryGroup>();
            ServiceProviderLogs = new HashSet<ServiceProviderLog>();
            DeliveryCharges = new HashSet<DeliveryCharge>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ServiceProviderID { get; set; }

        [StringLength(20)]
        public string ProviderCode { get; set; }

        [StringLength(50)]
        public string ProviderName { get; set; }

        public int? CountryID { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        [StringLength(500)]
        public string ServiceProviderLink { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceProviderCountryGroup> ServiceProviderCountryGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceProviderLog> ServiceProviderLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeliveryCharge> DeliveryCharges { get; set; }

        public virtual Country Country { get; set; }
    }
}
