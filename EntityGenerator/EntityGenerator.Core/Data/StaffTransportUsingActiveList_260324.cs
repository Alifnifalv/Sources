using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StaffTransportUsingActiveList_260324", Schema = "schools")]
    public partial class StaffTransportUsingActiveList_260324
    {
        public long StaffRouteStopMapIID { get; set; }
        public long? StaffID { get; set; }
        public long? RouteStopMapID { get; set; }
        public long? PickupStopMapID { get; set; }
        public long? DropStopMapID { get; set; }
        public bool? IsOneWay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? PickupRouteID { get; set; }
        public int? DropStopRouteID { get; set; }
        public bool? TermsAndConditions { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? TransportStatusID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CancelDate { get; set; }
        public int? IsRouteShifted { get; set; }
        public int? ShiftFromRouteID { get; set; }
    }
}
