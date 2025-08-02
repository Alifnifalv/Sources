using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VehicleStaffMapReportView
    {
        [StringLength(50)]
        public string VehicleRegistrationNumber { get; set; }
        [StringLength(20)]
        public string VehicleCode { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateFrom { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string DateTo { get; set; }
        [StringLength(502)]
        public string Driver { get; set; }
        [StringLength(50)]
        public string RouteDescription { get; set; }
        [StringLength(20)]
        public string RouteCode { get; set; }
    }
}
