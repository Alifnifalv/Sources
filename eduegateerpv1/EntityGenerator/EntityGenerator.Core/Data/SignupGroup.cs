using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
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
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DueDate { get; set; }

        [InverseProperty("SignupGroup")]
        public virtual ICollection<Signup> Signups { get; set; }
    }
}
