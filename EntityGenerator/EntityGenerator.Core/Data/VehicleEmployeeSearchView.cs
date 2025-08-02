using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class VehicleEmployeeSearchView
    {
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        public long AssignVehicleMapIID { get; set; }
        public long? VehicleID { get; set; }
        [StringLength(50)]
        public string Vehicle { get; set; }
        [StringLength(100)]
        public string RouteCode { get; set; }
        [StringLength(15)]
        public string ContactNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public long? DriverID { get; set; }
        [StringLength(555)]
        public string DriverName { get; set; }
        [StringLength(250)]
        public string Notes { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? RouteID { get; set; }
        public string AttenderName { get; set; }
        public string AttenderID { get; set; }
    }
}
