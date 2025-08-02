using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentPickerStudentMaps", Schema = "schools")]
    public partial class StudentPickerStudentMap
    {
        [Key]
        public long StudentPickerStudentMapIID { get; set; }
        public long? StudentPickerID { get; set; }
        public long? StudentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public string QRCODE { get; set; }
        public bool? IsActive { get; set; }
        public long? PickUpLoginID { get; set; }

        [ForeignKey("StudentID")]
        [InverseProperty("StudentPickerStudentMaps")]
        public virtual Student Student { get; set; }
        [ForeignKey("StudentPickerID")]
        [InverseProperty("StudentPickerStudentMaps")]
        public virtual StudentPicker StudentPicker { get; set; }
    }
}
