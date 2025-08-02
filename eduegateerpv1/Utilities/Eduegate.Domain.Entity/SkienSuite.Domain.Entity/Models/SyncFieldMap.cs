using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SyncFieldMap
    {
        public int SyncFieldMapID { get; set; }
        public Nullable<int> SynchFieldMapTypeID { get; set; }
        public string SourceField { get; set; }
        public string DestinationField { get; set; }
        public virtual SyncFieldMapType SyncFieldMapType { get; set; }
    }
}
