using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("EntityChangeTrackerLog", Schema = "sync")]
    public partial class EntityChangeTrackerLog
    {
        [Key]
        public long EntityChangeTrackerLogID { get; set; }

        public long EntityChangeTrackerType { get; set; }

        public long EntityChangeTrackerTypeID { get; set; }

        public DateTime CreatedDate { get; set; }

        public Nullable<System.DateTime> SyncedOn { get; set; }
    }
}
