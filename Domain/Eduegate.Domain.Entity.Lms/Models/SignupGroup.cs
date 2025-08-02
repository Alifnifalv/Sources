using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Lms.Models
{
    [Table("SignupGroups", Schema = "signup")]
    public partial class SignupGroup
    {
        public SignupGroup()
        {
            Signups = new HashSet<Signup>();
        }

        [Key]
        public int SignupGroupID { get; set; }

        [StringLength(100)]
        public string GroupTitle { get; set; }

        public string GroupDescription { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public DateTime? DueDate { get; set; }

        public bool? IsActive { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Signup> Signups { get; set; }
    }
}