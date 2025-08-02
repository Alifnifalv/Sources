namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.Vehicles")]
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            JobEntryHeads = new HashSet<JobEntryHead>();
            AssignVehicleMaps = new HashSet<AssignVehicleMap>();
            VehicleDetailMaps = new HashSet<VehicleDetailMap>();
            DriverScheduleLogs = new HashSet<DriverScheduleLog>();
            EventTransportAllocations = new HashSet<EventTransportAllocation>();
            RouteVehicleMaps = new HashSet<RouteVehicleMap>();
            StudentVehicleAssigns = new HashSet<StudentVehicleAssign>();
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public int? RigistrationCountryID { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(100)]
        public string FleetCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AssignVehicleMap> AssignVehicleMaps { get; set; }

        public virtual City City { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleDetailMap> VehicleDetailMaps { get; set; }

        public virtual VehicleOwnershipType VehicleOwnershipType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DriverScheduleLog> DriverScheduleLogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTransportAllocation> EventTransportAllocations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RouteVehicleMap> RouteVehicleMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentVehicleAssign> StudentVehicleAssigns { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual School School { get; set; }

        public virtual VehicleTransmission VehicleTransmission { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
