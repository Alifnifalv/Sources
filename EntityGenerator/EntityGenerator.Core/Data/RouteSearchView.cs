using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class RouteSearchView
    {
        public int RouteID { get; set; }
        [StringLength(100)]
        public string RouteCode { get; set; }
        public byte? RouteTypeID { get; set; }
        [StringLength(50)]
        public string RouteType { get; set; }
        public byte? RouteSchoolID { get; set; }
        public byte? SchoolID { get; set; }
        public int? RouteAcademicYearID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(200)]
        public string CreatedUserName { get; set; }
        [StringLength(200)]
        public string UpdatedUserName { get; set; }
        [StringLength(50)]
        public string RouteDescription { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteFareOneWay { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteFareTwoWay { get; set; }
        [Required]
        [StringLength(8)]
        [Unicode(false)]
        public string IsActive { get; set; }
        [Required]
        [StringLength(5)]
        [Unicode(false)]
        public string RowCategory { get; set; }
    }
}
