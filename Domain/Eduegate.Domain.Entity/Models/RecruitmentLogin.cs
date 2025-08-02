using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eduegate.Domain.Entity.Models;
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
        public string OTP { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<JobSeeker> JobSeekers { get; set; }
    }
}
