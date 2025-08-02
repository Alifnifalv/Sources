namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentPickupRequests")]
    public partial class StudentPickupRequest
    {
        [Key]
        public long StudentPickupRequestIID { get; set; }

        public long? StudentID { get; set; }

        public DateTime? RequestDate { get; set; }

        public byte? RequestStatusID { get; set; }

        public byte? PickedByID { get; set; }

        public long? ParentID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string AdditionalInfo { get; set; }

        [StringLength(50)]
        public string RequestCode { get; set; }

        public byte[] RequestCodeImage { get; set; }

        public long? PhotoContentID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? AcademicYearID { get; set; }

        public byte? SchoolID { get; set; }

        public DateTime? PickedDate { get; set; }

        public TimeSpan? FromTime { get; set; }

        public TimeSpan? ToTime { get; set; }

        public long? StudentPickerID { get; set; }

        public int? ClassID { get; set; }

        public int? SectionID { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Class Class { get; set; }

        public virtual School School { get; set; }

        public virtual Section Section { get; set; }

        public virtual StudentPickedBy StudentPickedBy { get; set; }

        public virtual StudentPicker StudentPicker { get; set; }

        public virtual StudentPickupRequestStatus StudentPickupRequestStatus { get; set; }

        public virtual Student Student { get; set; }
    }
}
