using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("RecruitmentLogins", Schema = "hr")]
    public partial class RecruitmentLogin
    {
        public RecruitmentLogin()
        {
            JobSeekers = new HashSet<JobSeeker>();
        }

        [Key]
        public long LoginID { get; set; }
        public string UserID { get; set; }
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public string PasswordHint { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleID { get; set; }
        [StringLength(250)]
        public string OTP { get; set; }
        public int? CreatedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("RoleID")]
        [InverseProperty("RecruitmentLogins")]
        public virtual Role Role { get; set; }
        [InverseProperty("Login")]
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
    }
}
