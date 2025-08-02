using Eduegate.Domain.Entity.Accounts.Models.Accounts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Accounts.Models.Jobs
{
    [Table("JobsEntryHeadPayableMaps", Schema = "jobs")]
    public partial class JobsEntryHeadPayableMap
    {
        [Key]
        public long JobsEntryHeadPayableMapIID { get; set; }

        public long? PayableID { get; set; }

        public long? JobEntryHeadID { get; set; }

        public virtual JobEntryHead JobEntryHead { get; set; }

        public virtual Payable Payable { get; set; }
    }
}