using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
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

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Signup> Signups { get; set; }

    }
}