using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("SyncFieldMapTypes", Schema = "sync")]
    public partial class SyncFieldMapType
    {
        public SyncFieldMapType()
        {
            this.SyncFieldMaps = new List<SyncFieldMap>();
        }

        [Key]
        public int SynchFieldMapTypeID { get; set; }
        public string MapName { get; set; }
        public virtual ICollection<SyncFieldMap> SyncFieldMaps { get; set; }
    }
}
