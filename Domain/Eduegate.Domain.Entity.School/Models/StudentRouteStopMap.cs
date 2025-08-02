using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.School.Models
{
    [Table("StudentRouteStopMaps", Schema = "schools")]
    public partial class StudentRouteStopMap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentRouteStopMap()
        {
            StudentRouteMonthlySplits = new HashSet<StudentRouteMonthlySplit>();
            StudentRouteStopMapLogs = new HashSet<StudentRouteStopMapLog>();
            StudentRoutePeriodMaps = new HashSet<StudentRoutePeriodMap>();
            EventTransportAllocationMaps = new HashSet<EventTransportAllocationMap>();
            TransportCancelRequests = new HashSet<TransportCancelRequest>();
        }

        [Key]
        public long StudentRouteStopMapIID { get; set; }

        public long? StudentID { get; set; }

        public long? RouteStopMapID { get; set; }

        public long? PickupStopMapID { get; set; }

        public long? DropStopMapID { get; set; }

        public bool? IsOneWay { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //[Column(TypeName = "timestamp")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[MaxLength(8)]
        ////public byte[] TimeStamps { get; set; }

        public int? PickupRouteID { get; set; }

        public int? DropStopRouteID { get; set; }

        public bool? Termsandco { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public long? TransportStatusID { get; set; }
        public int? IsRouteShifted { get; set; }

        public string Remarks { get; set; }

        public long? TransportApplctnStudentMapID { get; set; }
       
        public DateTime? CancelDate { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Schools School { get; set; }

        public virtual Routes1 Routes1 { get; set; }

        public virtual Routes1 Routes11 { get; set; }

        public virtual RouteStopMap RouteStopMap { get; set; }

        public virtual RouteStopMap RouteStopMap1 { get; set; }

        public virtual RouteStopMap RouteStopMap2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRoutePeriodMap> StudentRoutePeriodMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteMonthlySplit> StudentRouteMonthlySplits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventTransportAllocationMap> EventTransportAllocationMaps { get; set; }

        public virtual Student Student { get; set; }

        public virtual Class Class { get; set; }

        public virtual Section Section { get; set; }

        public virtual TransportStatus TransportStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentRouteStopMapLog> StudentRouteStopMapLogs { get; set; }

        public virtual ICollection<TransportCancelRequest> TransportCancelRequests { get; set; }

    }
}