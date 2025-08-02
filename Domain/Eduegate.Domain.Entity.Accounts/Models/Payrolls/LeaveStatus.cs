using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Payrolls
{
    [Table("LeaveStatuses", Schema = "payroll")]
    public partial class LeaveStatus
    {
        public LeaveStatus()
        {
        }

        [Key]
        public byte LeaveStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }
    }
}