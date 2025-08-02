using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Payrolls
{
    [Table("LeaveSessions", Schema = "payroll")]
    public partial class LeaveSession
    {
        public LeaveSession()
        {
        }

        [Key]
        public byte LeaveSessionID { get; set; }

        [StringLength(50)]
        public string SesionName { get; set; }
    }
}