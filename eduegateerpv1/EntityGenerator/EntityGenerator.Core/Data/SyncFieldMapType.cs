using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SyncFieldMapTypes", Schema = "sync")]
    public partial class SyncFieldMapType
    {
        public SyncFieldMapType()
        {
            SyncFieldMaps = new HashSet<SyncFieldMap>();
        }

        [Key]
        public int SynchFieldMapTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string MapName { get; set; }

        [InverseProperty("SynchFieldMapType")]
        public virtual ICollection<SyncFieldMap> SyncFieldMaps { get; set; }
    }
}
