using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AdminLogins_20220812
    {
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
        [Column(TypeName = "datetime")]
        public DateTime? LastLoginDate { get; set; }
        public bool? RequirePasswordReset { get; set; }
        public int? SiteID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string LastOTP { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? SchoolID { get; set; }
        public bool? IsForceLogout { get; set; }
        public bool? EnableForceLogout { get; set; }
        public bool? ExternalReference { get; set; }
    }
}
