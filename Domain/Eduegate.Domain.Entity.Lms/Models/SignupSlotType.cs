using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("SignupSlotTypes", Schema = "signup")]
    public partial class SignupSlotType
    {
        public SignupSlotType()
        {
            SignupSlotMaps = new HashSet<SignupSlotMap>();
        }

        [Key]
        public byte SignupSlotTypeID { get; set; }

        [StringLength(100)]
        public string SignupSlotTypeName { get; set; }

        public byte? SchoolID { get; set; }

        public int? AcademicYearID { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }

    }
}