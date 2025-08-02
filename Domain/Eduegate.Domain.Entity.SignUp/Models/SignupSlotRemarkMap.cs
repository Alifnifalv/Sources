using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.SignUp.Models
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public DateTime? RemarkEnteredDate { get; set; }

        public virtual Signup Signup { get; set; }

        public virtual SignupSlotAllocationMap SignupSlotAllocationMap { get; set; }

        public virtual SignupSlotMap SignupSlotMap { get; set; }
    }
}