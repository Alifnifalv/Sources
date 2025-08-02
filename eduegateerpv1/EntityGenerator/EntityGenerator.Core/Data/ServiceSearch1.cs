using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ServiceSearch1
    {
        public long ServiceIID { get; set; }
        public long? ParentServiceID { get; set; }
        [StringLength(100)]
        public string ServiceCode { get; set; }
        [StringLength(1000)]
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int? TreatmentTypeID { get; set; }
        [StringLength(200)]
        public string TreatmentName { get; set; }
        public int? ServiceAvailableID { get; set; }
        [StringLength(100)]
        public string ServiceAvailable { get; set; }
        public int? PricingTypeID { get; set; }
        public int? ExtraTimeTypeID { get; set; }
        [StringLength(100)]
        public string ExtraTimeTypes { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ExtratimeDuration { get; set; }
        public long? ServiceGroupID { get; set; }
        [StringLength(200)]
        public string GroupName { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Updated { get; set; }
    }
}
