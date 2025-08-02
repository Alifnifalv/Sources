namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.SyncFieldMaps")]
    public partial class SyncFieldMap
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SyncFieldMapID { get; set; }

        public int? SynchFieldMapTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string SourceField { get; set; }

        [StringLength(50)]
        public string DestinationField { get; set; }

        public virtual SyncFieldMapType SyncFieldMapType { get; set; }
    }
}
