using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceSearch1
    {
        public long ServiceIID { get; set; }
        public Nullable<long> ParentServiceID { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public Nullable<int> TreatmentTypeID { get; set; }
        public string TreatmentName { get; set; }
        public Nullable<int> ServiceAvailableID { get; set; }
        public string ServiceAvailable { get; set; }
        public Nullable<int> PricingTypeID { get; set; }
        public Nullable<int> ExtraTimeTypeID { get; set; }
        public string ExtraTimeTypes { get; set; }
        public Nullable<decimal> ExtratimeDuration { get; set; }
        public Nullable<long> ServiceGroupID { get; set; }
        public string GroupName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
    }
}
