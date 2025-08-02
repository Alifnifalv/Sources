using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TransportRouteDetailReportView
    {
        public int RouteID { get; set; }
        [StringLength(100)]
        public string RouteCode { get; set; }
        public byte? RouteTypeID { get; set; }
        [StringLength(50)]
        public string RouteDescription { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteFareOneWay { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteFareTwoWay { get; set; }
        public int? CostCenterID { get; set; }
        [StringLength(50)]
        public string Landmark { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(15)]
        public string ContactNumber { get; set; }
        public bool? IsActive { get; set; }
        public long? RouteStopMapIID { get; set; }
        [StringLength(50)]
        public string StopName { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? OneWayFee { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? TwoWayFee { get; set; }
        [StringLength(20)]
        public string StopCode { get; set; }
    }
}
