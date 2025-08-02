namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sync.EntityChangeTrackerBatch")]
    public partial class EntityChangeTrackerBatch
    {
        public int EntityChangeTrackerBatchID { get; set; }

        [Required]
        [StringLength(100)]
        public string EntityChangeTrackerBatchNo { get; set; }

        public int NoOfProducts { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
