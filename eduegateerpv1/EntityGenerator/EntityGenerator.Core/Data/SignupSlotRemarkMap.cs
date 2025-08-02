using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SignupSlotRemarkMaps", Schema = "signup")]
    public partial class SignupSlotRemarkMap
    {
        [Key]
        public long SignupSlotRemarkMapIID { get; set; }
        public long? SignupSlotAllocationMapID { get; set; }
        public long? SignupSlotMapID { get; set; }
        public long? SignupID { get; set; }
        public string TeacherRemarks { get; set; }
        public string ParentRemarks { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("SignupID")]
        [InverseProperty("SignupSlotRemarkMaps")]
        public virtual Signup Signup { get; set; }
        [ForeignKey("SignupSlotAllocationMapID")]
        [InverseProperty("SignupSlotRemarkMaps")]
        public virtual SignupSlotAllocationMap SignupSlotAllocationMap { get; set; }
        [ForeignKey("SignupSlotMapID")]
        [InverseProperty("SignupSlotRemarkMaps")]
        public virtual SignupSlotMap SignupSlotMap { get; set; }
    }
}
