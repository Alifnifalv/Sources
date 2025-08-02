using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Services", Schema = "saloon")]
    public partial class Service
    {
        public Service()
        {
            Appointments = new HashSet<Appointment>();
            InverseParentService = new HashSet<Service>();
            ServiceEmployeeMaps = new HashSet<ServiceEmployeeMap>();
            ServicePricings = new HashSet<ServicePricing>();
        }

        [Key]
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
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ExtratimeDuration { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Updated { get; set; }

        [ForeignKey("ExtraTimeTypeID")]
        [InverseProperty("Services")]
        public virtual ExtraTimeType ExtraTimeType { get; set; }
        [ForeignKey("ParentServiceID")]
        [InverseProperty("InverseParentService")]
        public virtual Service ParentService { get; set; }
        [ForeignKey("PricingTypeID")]
        [InverseProperty("Services")]
        public virtual PricingType PricingType { get; set; }
        [ForeignKey("ServiceAvailableID")]
        [InverseProperty("Services")]
        public virtual ServiceAvailable ServiceAvailable { get; set; }
        [ForeignKey("ServiceGroupID")]
        [InverseProperty("Services")]
        public virtual ServiceGroup ServiceGroup { get; set; }
        [ForeignKey("TreatmentTypeID")]
        [InverseProperty("Services")]
        public virtual TreatmentType TreatmentType { get; set; }
        [InverseProperty("Service")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [InverseProperty("ParentService")]
        public virtual ICollection<Service> InverseParentService { get; set; }
        [InverseProperty("Service")]
        public virtual ICollection<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }
        [InverseProperty("Service")]
        public virtual ICollection<ServicePricing> ServicePricings { get; set; }
    }
}
