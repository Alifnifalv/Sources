using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SyncFieldMapType
    {
        public SyncFieldMapType()
        {
            this.SyncFieldMaps = new List<SyncFieldMap>();
        }

        public int SynchFieldMapTypeID { get; set; }
        public string MapName { get; set; }
        public virtual ICollection<SyncFieldMap> SyncFieldMaps { get; set; }
    }
}
