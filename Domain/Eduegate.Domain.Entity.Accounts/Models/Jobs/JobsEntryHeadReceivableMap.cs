using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
{
    [Table("JobsEntryHeadReceivableMaps", Schema = "jobs")]
    public partial class JobsEntryHeadReceivableMap
    {
        [Key]
        public long JobsEntryHeadReceivableMapIID { get; set; }

        public long? ReceivableID { get; set; }

        public long? JobEntryHeadID { get; set; }

        public virtual JobEntryHead JobEntryHead { get; set; }

        public virtual Receivable Receivable { get; set; }
    }
}