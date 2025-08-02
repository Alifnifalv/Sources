using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Eduegate.Domain.Entity.SignUp.Models
{
    [Table("Logins", Schema = "admin")]
    public partial class Login
    {
        public Login()
        {
            Employees = new HashSet<Employee>();
            Parents = new HashSet<Parent>();
            Students = new HashSet<Student>();
        }

        [Key]
        public long LoginIID { get; set; }

        [StringLength(100)]
        public string LoginUserID { get; set; }

        [StringLength(100)]
        public string LoginEmailID { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(1000)]
        public string ProfileFile { get; set; }

        public long? CustomerID { get; set; }

        public long? SupplierID { get; set; }

        public long? EmployeeID { get; set; }

        [StringLength(50)]
        public string PasswordHint { get; set; }

        [StringLength(128)]
        [Unicode(false)]
        public string PasswordSalt { get; set; }

        [StringLength(128)]
        public string Password { get; set; }

        public byte? StatusID { get; set; }

        public int? RegisteredCountryID { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string RegisteredIP { get; set; }

        [StringLength(20)]
        [Unicode(false)]
        public string RegisteredIPCountry { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public bool? RequirePasswordReset { get; set; }

        public int? SiteID { get; set; }

        [StringLength(50)]
        [Unicode(false)]
        public string LastOTP { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public byte? SchoolID { get; set; }

        public bool? IsForceLogout { get; set; }

        public bool? EnableForceLogout { get; set; }

        public bool? ExternalReference { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        
        public virtual ICollection<Parent> Parents { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }
        
    }
}