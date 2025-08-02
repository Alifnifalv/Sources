using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("TenderAuthentication", Schema = "inventory")]
    public partial class TenderAuthentication
    {
        public TenderAuthentication()
        {
            Tenders = new HashSet<Tender>();
        }

        [Key]
        public long AuthenticationID { get; set; }
        public long? LoginID { get; set; }
        public string UserID { get; set; }
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public string PasswordHint { get; set; }
        public string PasswordSalt { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public long? TenderID { get; set; }
        public bool? IsApprover { get; set; }

        [ForeignKey("TenderID")]
        [InverseProperty("TenderAuthentications")]
        public virtual Tender Tender { get; set; }
        [InverseProperty("TenderAwarded")]
        public virtual ICollection<Tender> Tenders { get; set; }
    }
}
