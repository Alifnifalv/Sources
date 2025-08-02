using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("AssignVehicleMap", Schema = "mutual")]
    public partial class AssignVehicleMap
    {
        public AssignVehicleMap()
        {
            AssignVehicleAttendantMaps = new HashSet<AssignVehicleAttendantMap>();
        }

        [Key]
        public long AssignVehicleMapIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public long? EmployeeID { get; set; }
        public long? VehicleID { get; set; }
        [StringLength(250)]
        public string Notes { get; set; }
        public bool? IsActive { get; set; }
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
        [InverseProperty("AssignVehicleMaps")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("AssignVehicleMaps")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("RouteID")]
        [InverseProperty("AssignVehicleMaps")]
        public virtual Route1 Route { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("AssignVehicleMaps")]
        public virtual School School { get; set; }
        [ForeignKey("VehicleID")]
        [InverseProperty("AssignVehicleMaps")]
        public virtual Vehicle Vehicle { get; set; }
        [InverseProperty("AssignVehicleMap")]
        public virtual ICollection<AssignVehicleAttendantMap> AssignVehicleAttendantMaps { get; set; }
    }
}
