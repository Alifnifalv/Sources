using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class RoutevehicleMapReportView
    {
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(20)]
        public string VehicleCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(20)]
        public string RouteCode { get; set; }
        [StringLength(50)]
        public string RouteDescription { get; set; }
        [StringLength(50)]
        public string VehicleRegistrationNumber { get; set; }
        [StringLength(50)]
        public string Expr1 { get; set; }
    }
}
