using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TenderAuthenticationLog", Schema = "inventory")]
    public partial class TenderAuthenticationLog
    {
        [Key]
        public long TenderAuthMapIID { get; set; }
        public long? TenderID { get; set; }
        public long? AuthenticationID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsTenderApproved { get; set; }
        public bool? IsTenderOpened { get; set; }
        public string LastOTP { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OpenedDate { get; set; }
        public string Remarks { get; set; }
        public bool? IsApprover { get; set; }

        [ForeignKey("AuthenticationID")]
        [InverseProperty("TenderAuthenticationLogs")]
        public virtual TenderAuthentication Authentication { get; set; }
        [ForeignKey("TenderID")]
        [InverseProperty("TenderAuthenticationLogs")]
        public virtual Tender Tender { get; set; }
    }
}
