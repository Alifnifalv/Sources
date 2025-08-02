using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityChangeTrackerLog", Schema = "sync")]
    public partial class EntityChangeTrackerLog
    {
        [Key]
        public long EntityChangeTrackerLogID { get; set; }
        /// <summary>
        /// 0 - Category
        /// 1 - Brand
        /// 2 - Supplier
        /// </summary>
        public long EntityChangeTrackerType { get; set; }
        public long EntityChangeTrackerTypeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SyncedOn { get; set; }
    }
}
