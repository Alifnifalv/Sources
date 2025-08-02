using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class TransportDetailReportView
    {
        public long? STUD_STAFF_ID { get; set; }
        [Required]
        [StringLength(502)]
        public string STUD_EMP_Name { get; set; }
        [StringLength(50)]
        public string ADM_EMP_CODE { get; set; }
        [StringLength(101)]
        public string CLASS_SEC_Name { get; set; }
        [StringLength(50)]
        public string PickUpStopName { get; set; }
        [StringLength(50)]
        public string DropStopName { get; set; }
        [Required]
        [StringLength(7)]
        [Unicode(false)]
        public string IsOneWay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateTo { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(153)]
        public string PickUpRoute { get; set; }
        [StringLength(153)]
        public string DropRoute { get; set; }
        [StringLength(125)]
        public string AcademicYear { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public long? PickVehicleID { get; set; }
        public long? DropVehiclID { get; set; }
        [StringLength(100)]
        public string TransportStatus { get; set; }
        [StringLength(7)]
        [Unicode(false)]
        public string Student_Staff { get; set; }
    }
}
