using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Vehicles", Schema = "mutual")]
    public partial class Vehicle
    {
        public Vehicle()
        {
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            EventTransportAllocations = new HashSet<EventTransportAllocation>();
            JobEntryHeads = new HashSet<JobEntryHead>();
            RouteVehicleMaps = new HashSet<RouteVehicleMap>();
            StudentVehicleAssigns = new HashSet<StudentVehicleAssign>();
            VehicleDetailMaps = new HashSet<VehicleDetailMap>();
            VehicleTrackings = new HashSet<VehicleTracking>();
        }

        [Key]
        public long VehicleIID { get; set; }
        public short? VehicleTypeID { get; set; }
        public short? VehicleOwnershipTypeID { get; set; }
        [StringLength(50)]
        public string VehicleRegistrationNumber { get; set; }
        [StringLength(20)]
        public string VehicleCode { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        [StringLength(50)]
        public string RegistrationNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PurchaseDate { get; set; }
        [StringLength(50)]
        public string ModelName { get; set; }
        public int? YearMade { get; set; }
        public int? VehicleAge { get; set; }
        public byte? TransmissionID { get; set; }
        [StringLength(50)]
        public string ManufactureName { get; set; }
        [StringLength(50)]
        public string Color { get; set; }
        [StringLength(50)]
        public string Power { get; set; }
        public int? AllowSeatingCapacity { get; set; }
        public int? MaximumSeatingCapacity { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSecurityEnabled { get; set; }
        public bool? IsCameraEnabled { get; set; }
        public int? RigistrationCityID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public int? RigistrationCountryID { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        [StringLength(100)]
        public string FleetCode { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("Vehicles")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("RigistrationCityID")]
        [InverseProperty("Vehicles")]
        public virtual City RigistrationCity { get; set; }
        [ForeignKey("RigistrationCountryID")]
        [InverseProperty("Vehicles")]
        public virtual Country RigistrationCountry { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("Vehicles")]
        public virtual School School { get; set; }
        [ForeignKey("TransmissionID")]
        [InverseProperty("Vehicles")]
        public virtual VehicleTransmission Transmission { get; set; }
        [ForeignKey("VehicleOwnershipTypeID")]
        [InverseProperty("Vehicles")]
        public virtual VehicleOwnershipType VehicleOwnershipType { get; set; }
        [ForeignKey("VehicleTypeID")]
        [InverseProperty("Vehicles")]
        public virtual VehicleType VehicleType { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<EventTransportAllocation> EventTransportAllocations { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<RouteVehicleMap> RouteVehicleMaps { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<StudentVehicleAssign> StudentVehicleAssigns { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<VehicleDetailMap> VehicleDetailMaps { get; set; }
        [InverseProperty("Vehicle")]
        public virtual ICollection<VehicleTracking> VehicleTrackings { get; set; }
    }
}
