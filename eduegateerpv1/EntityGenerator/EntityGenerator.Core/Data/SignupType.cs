using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SignupTypes", Schema = "signup")]
    public partial class SignupType
    {
        public SignupType()
        {
            Signups = new HashSet<Signup>();
        }

        [Key]
        public byte SignupTypeID { get; set; }
        [StringLength(100)]
        public string SignupTypeName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("SignupType")]
        public virtual ICollection<Signup> Signups { get; set; }
    }
}
