using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentPickers", Schema = "schools")]
    public partial class StudentPicker
    {
        public StudentPicker()
        {
            StudentPickLogs = new HashSet<StudentPickLog>();
            StudentPickerStudentMaps = new HashSet<StudentPickerStudentMap>();
        }

        [Key]
        public long StudentPickerIID { get; set; }
        public long? ParentID { get; set; }
        public byte? PickedByID { get; set; }
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
        public string VisitorCode { get; set; }

        [ForeignKey("AcademicYearID")]
        [InverseProperty("StudentPickers")]
        public virtual AcademicYear AcademicYear { get; set; }
        [ForeignKey("ParentID")]
        [InverseProperty("StudentPickers")]
        public virtual Parent Parent { get; set; }
        [ForeignKey("PickedByID")]
        [InverseProperty("StudentPickers")]
        public virtual StudentPickedBy PickedBy { get; set; }
        [ForeignKey("SchoolID")]
        [InverseProperty("StudentPickers")]
        public virtual School School { get; set; }
        [InverseProperty("StudentPicker")]
        public virtual ICollection<StudentPickLog> StudentPickLogs { get; set; }
        [InverseProperty("StudentPicker")]
        public virtual ICollection<StudentPickerStudentMap> StudentPickerStudentMaps { get; set; }
    }
}
