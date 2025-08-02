using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("StudentStaffMaps", Schema = "schools")]
    public partial class StudentStaffMap
    {
        [Key]
        public long StudentStaffMapIID { get; set; }
        public long StudentID { get; set; }
        public long? StaffID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("StaffID")]
        [InverseProperty("StudentStaffMaps")]
        public virtual Employee Staff { get; set; }
        [ForeignKey("StudentID")]
        [InverseProperty("StudentStaffMaps")]
        public virtual Student Student { get; set; }
    }
}
