using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentTransportDetailReportView
    {
        [Required]
        [StringLength(6)]
        [Unicode(false)]
        public string IsOneWay { get; set; }
        [StringLength(20)]
        public string PickupVehicleCode { get; set; }
        [StringLength(50)]
        public string PickupVehicleRegistrationNo { get; set; }
        [StringLength(20)]
        public string PickupRouteCode { get; set; }
        [StringLength(50)]
        public string PickupRoute { get; set; }
        [StringLength(50)]
        public string PickupStop { get; set; }
        [StringLength(20)]
        public string DropVehicleCode { get; set; }
        [StringLength(50)]
        public string DropVehicleRegistrationNo { get; set; }
        [StringLength(20)]
        public string DropRouteCode { get; set; }
        [StringLength(50)]
        public string DropRoute { get; set; }
        [StringLength(50)]
        public string DropStop { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [StringLength(502)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(50)]
        public string ClassDescription { get; set; }
        public int? ClassID { get; set; }
    }
}
