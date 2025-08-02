using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("VehicleTracking", Schema = "schools")]
    public partial class VehicleTracking
    {
        [Key]
        public long VehicleTrackingIID { get; set; }
        public long VehicleID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndTime { get; set; }
        public long? EmployeeID { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteStartKM { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? RouteEndKM { get; set; }
        [StringLength(50)]
        public string AttachmentID1 { get; set; }
        [StringLength(50)]
        public string AttachmentID2 { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public int? RouteID { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("VehicleTrackings")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("VehicleTrackings")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("RouteID")]
        [InverseProperty("VehicleTrackings")]
        public virtual Route1 Route { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("VehicleTrackings")]
        public virtual School School { get; set; }
        [ForeignKey("VehicleID")]
        [InverseProperty("VehicleTrackings")]
        public virtual Vehicle Vehicle { get; set; }
    }
}
