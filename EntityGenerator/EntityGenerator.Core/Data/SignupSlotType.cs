using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("SignupSlotType")]
        public virtual ICollection<SignupSlotMap> SignupSlotMaps { get; set; }
    }
}
