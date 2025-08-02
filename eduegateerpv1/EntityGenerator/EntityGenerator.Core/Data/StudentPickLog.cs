using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentPickLogs", Schema = "schools")]
    public partial class StudentPickLog
    {
        [Key]
        public long StudentPickLogIID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PickDate { get; set; }
        public long StudentPickerID { get; set; }
        public long? StudentID { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        public long? PhotoContentID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public bool? LogStatus { get; set; }

        [ForeignKey("StudentID")]
        [InverseProperty("StudentPickLogs")]
        public virtual Student Student { get; set; }
        [ForeignKey("StudentPickerID")]
        [InverseProperty("StudentPickLogs")]
        public virtual StudentPicker StudentPicker { get; set; }
    }
}
