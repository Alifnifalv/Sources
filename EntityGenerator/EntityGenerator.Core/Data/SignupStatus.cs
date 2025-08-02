using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SignupStatuses", Schema = "signup")]
    public partial class SignupStatus
    {
        public SignupStatus()
        {
            Signups = new HashSet<Signup>();
        }

        [Key]
        public byte SignupStatusID { get; set; }
        [StringLength(100)]
        public string SignupStatusName { get; set; }
        public int? StatusOrder { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty("SignupStatus")]
        public virtual ICollection<Signup> Signups { get; set; }
    }
}
