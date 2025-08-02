using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Service
    {
        public Service()
        {
            this.ServiceEmployeeMaps = new List<ServiceEmployeeMap>();
            this.ServicePricings = new List<ServicePricing>();
            this.Services1 = new List<Service>();
        }

        public long ServiceIID { get; set; }
        public Nullable<long> ParentServiceID { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public Nullable<long> ServiceGroupID { get; set; }
        public Nullable<int> TreatmentTypeID { get; set; }
        public Nullable<int> ServiceAvailableID { get; set; }
        public Nullable<int> PricingTypeID { get; set; }
        public Nullable<int> ExtraTimeTypeID { get; set; }
        public Nullable<decimal> ExtratimeDuration { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public virtual ExtraTimeType ExtraTimeType { get; set; }
        public virtual PricingType PricingType { get; set; }
        public virtual ServiceAvailable ServiceAvailable { get; set; }
        public virtual ICollection<ServiceEmployeeMap> ServiceEmployeeMaps { get; set; }
        public virtual ServiceGroup ServiceGroup { get; set; }
        public virtual ICollection<ServicePricing> ServicePricings { get; set; }
        public virtual ICollection<Service> Services1 { get; set; }
        public virtual Service Service1 { get; set; }
        public virtual TreatmentType TreatmentType { get; set; }
    }
}
