using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("StudentPickupRequests", Schema = "schools")]
    public partial class StudentPickupRequest
    {
        public long StudentPickupRequestIID { get; set; }
        public long? StudentID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RequestDate { get; set; }
        public byte? RequestStatusID { get; set; }
        public byte? PickedByID { get; set; }
        public long? ParentID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MiddleName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LastName { get; set; }
        [StringLength(500)]
        public string AdditionalInfo { get; set; }
        [StringLength(50)]
        public string RequestCode { get; set; }
        public byte[] RequestCodeImage { get; set; }
        public long? PhotoContentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? AcademicYearID { get; set; }
        public byte? SchoolID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PickedDate { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? ToTime { get; set; }
        public long? StudentPickerID { get; set; }
        public int? ClassID { get; set; }
        public int? SectionID { get; set; }

        [ForeignKey("AcademicYearID")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }
        [ForeignKey("PickedByID")]
        public virtual StudentPickedBy PickedBy { get; set; }
        [ForeignKey("RequestStatusID")]
        public virtual StudentPickupRequestStatus RequestStatus { get; set; }
        [ForeignKey("SchoolID")]
        public virtual School School { get; set; }
        [ForeignKey("SectionID")]
        public virtual Section Section { get; set; }
        [ForeignKey("StudentID")]
        public virtual Student Student { get; set; }
        [ForeignKey("StudentPickerID")]
        public virtual StudentPicker StudentPicker { get; set; }
    }
}
