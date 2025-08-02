namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("saloon.Services")]
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            Appointments = new HashSet<Appointment>();
            ServiceEmployeeMaps = new HashSet<ServiceEmployeeMap>();
            ServicePricings = new HashSet<ServicePricing>();
            Services1 = new HashSet<Service>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ServiceIID { get; set; }

        public long? ParentServiceID { get; set; }

        [StringLength(100)]
        public string ServiceCode { get; set; }

        [StringLength(1000)]
        public string ServiceName { get; set; }

        public string ServiceDescription { get; set; }

        public long? ServiceGroupID { get; set; }

        public int? TreatmentTypeID { get; set; }

        public int? ServiceAvailableID { get; set; }

        public int? PricingTypeID { get; set; }

        public int? ExtraTimeTypeID { get; set; }

        public decimal? ExtratimeDuration { get; set; }

        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ExtraTimeType ExtraTimeType { get; set; }

        public virtual PricingType PricingType { get; set; }

        public virtual ServiceAvailable ServiceAvailable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }

        public virtual ServiceGroup ServiceGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicePricing> ServicePricings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services1 { get; set; }

        public virtual Service Service1 { get; set; }

        public virtual TreatmentType TreatmentType { get; set; }
    }
}
