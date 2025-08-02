using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityChangeTrackersInProcess", Schema = "sync")]
    public partial class EntityChangeTrackersInProcess
    {
        [Key]
        public long EntityChangeTrackerInProcessIID { get; set; }
        public long? EntityChangeTrackerID { get; set; }
        public bool? IsReprocess { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EntityChangeTrackerID")]
        [InverseProperty("EntityChangeTrackersInProcesses")]
        public virtual EntityChangeTracker EntityChangeTracker { get; set; }
    }
}
