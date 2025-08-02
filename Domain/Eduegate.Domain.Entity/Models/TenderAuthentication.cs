using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("TenderAuthentication", Schema = "inventory")]
    public partial class TenderAuthentication
    {
        public TenderAuthentication()
        {
            TenderAuthenticationLogs = new HashSet<TenderAuthenticationLog>();
        }

        [Key]
        public long AuthenticationID { get; set; }

        public string UserID { get; set; }

        public string EmailID { get; set; }

        public string UserName { get; set; }

        public string PasswordHint { get; set; }

        public string PasswordSalt { get; set; }

        public string Password { get; set; }

        public bool? IsActive { get; set; }

        public long? EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<TenderAuthenticationLog> TenderAuthenticationLogs { get; set; }
    }
}
