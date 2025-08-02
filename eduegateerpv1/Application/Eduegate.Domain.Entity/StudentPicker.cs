namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("schools.StudentPickers")]
    public partial class StudentPicker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentPicker()
        {
            StudentPickerStudentMaps = new HashSet<StudentPickerStudentMap>();
            StudentPickupRequests = new HashSet<StudentPickupRequest>();
            StudentPickLogs = new HashSet<StudentPickLog>();
        }

        [Key]
        public long StudentPickerIID { get; set; }

        public long? ParentID { get; set; }

        public byte? PickedByID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string AdditionalInfo { get; set; }

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

        public string VisitorCode { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual School School { get; set; }

        public virtual StudentPickedBy StudentPickedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickerStudentMap> StudentPickerStudentMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickupRequest> StudentPickupRequests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentPickLog> StudentPickLogs { get; set; }
    }
}
