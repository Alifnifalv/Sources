using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("SyncFieldMaps", Schema = "sync")]
    public partial class SyncFieldMap
    {
        [Key]
        public int SyncFieldMapID { get; set; }
        public int? SynchFieldMapTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string SourceField { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DestinationField { get; set; }

        [ForeignKey("SynchFieldMapTypeID")]
        [InverseProperty("SyncFieldMaps")]
        public virtual SyncFieldMapType SynchFieldMapType { get; set; }
    }
}
