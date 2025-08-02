using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class StudentRouteSequenceWiseReportView
    {
        public int? pickUpsequence { get; set; }
        public int? DropSequence { get; set; }
        [StringLength(50)]
        public string AdmissionNumber { get; set; }
        public long StudentIID { get; set; }
        [Required]
        [StringLength(1)]
        [Unicode(false)]
        public string Gender { get; set; }
        [Required]
        [StringLength(502)]
        public string StudentName { get; set; }
        public byte? SchoolID { get; set; }
        public int? AcademicYearID { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(50)]
        public string PickupStopName { get; set; }
        [StringLength(50)]
        public string DropStopName { get; set; }
        [StringLength(100)]
        public string PickupRouteCode { get; set; }
        public int? PickupRouteID { get; set; }
        [StringLength(100)]
        public string DropRouteCode { get; set; }
        public int? DropStopRouteID { get; set; }
        public long? PickupStopMapID { get; set; }
        public long? DropStopMapID { get; set; }
        public int ClassID { get; set; }
        [StringLength(4000)]
        public string ClassDescription { get; set; }
        public int SectionID { get; set; }
        [StringLength(50)]
        public string SectionName { get; set; }
        [StringLength(123)]
        public string AcademicYear { get; set; }
        [StringLength(50)]
        public string SchoolName { get; set; }
        public long? ParentID { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MotherPhone { get; set; }
    }
}
