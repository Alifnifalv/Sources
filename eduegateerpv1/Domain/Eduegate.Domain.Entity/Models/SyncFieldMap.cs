using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("SyncFieldMaps", Schema = "sync")]
    public partial class SyncFieldMap
    {
        [Key]
        public int SyncFieldMapID { get; set; }
        public Nullable<int> SynchFieldMapTypeID { get; set; }
        public string SourceField { get; set; }
        public string DestinationField { get; set; }
        public virtual SyncFieldMapType SyncFieldMapType { get; set; }
    }
}
