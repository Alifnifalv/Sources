namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.TransportApplications")]
    public partial class TransportApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransportApplication()
        {
            TransportApplctnStudentMaps = new HashSet<TransportApplctnStudentMap>();
        }

        [Key]
        public long TransportApplicationIID { get; set; }

        [StringLength(50)]
        public string ApplicationNumber { get; set; }

        public long? LoginID { get; set; }

        public long? ParentID { get; set; }

        [StringLength(200)]
        public string LandMark { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public int? AcademicYearID { get; set; }

        [StringLength(50)]
        public string MotherContactNumber { get; set; }

        [StringLength(50)]
        public string MotherEmailID { get; set; }

        [Column("Building/FlatNo")]
        [StringLength(20)]
        public string Building_FlatNo { get; set; }

        [StringLength(50)]
        public string StreetNo { get; set; }

        [StringLength(50)]
        public string StreetName { get; set; }

        [StringLength(50)]
        public string LocationNo { get; set; }

        [StringLength(50)]
        public string LocationName { get; set; }

        [StringLength(50)]
        public string ZoneNo { get; set; }

        public short? ZoneID { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string EmergencyContactNumber { get; set; }

        [StringLength(50)]
        public string EmergencyEmailID { get; set; }

        public DateTime? PickUpTime { get; set; }

        public DateTime? DropOffTime { get; set; }

        [StringLength(75)]
        public string PickUpStop { get; set; }

        [StringLength(75)]
        public string DropOffStop { get; set; }

        public short? StreetID { get; set; }

        [StringLength(50)]
        public string FatherContactNumber { get; set; }

        [StringLength(50)]
        public string FatherEmailID { get; set; }

        public byte? SchoolID { get; set; }

        [StringLength(20)]
        public string BuildingNo_Drop { get; set; }

        [StringLength(50)]
        public string StreetNo_Drop { get; set; }

        [StringLength(100)]
        public string StreetName_Drop { get; set; }

        [StringLength(50)]
        public string LocationNo_Drop { get; set; }

        [StringLength(100)]
        public string LocationName_Drop { get; set; }

        [StringLength(50)]
        public string ZoneNo_Drop { get; set; }

        [StringLength(50)]
        public string LandMark_Drop { get; set; }

        public long? PickupStopMapID { get; set; }

        public long? DropStopMapID { get; set; }

        public bool? IsRouteDifferent { get; set; }

        public bool? IsNewStops { get; set; }

        public string Remarks { get; set; }

        public virtual Street Street { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual RouteStopMap RouteStopMap { get; set; }

        public virtual RouteStopMap RouteStopMap1 { get; set; }

        public virtual School School { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransportApplctnStudentMap> TransportApplctnStudentMaps { get; set; }
    }
}
