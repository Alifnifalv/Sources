using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityChangeTrackerBatch", Schema = "sync")]
    public partial class EntityChangeTrackerBatch
    {
        [Key]
        public int EntityChangeTrackerBatchID { get; set; }
        [Required]
        [StringLength(100)]
        public string EntityChangeTrackerBatchNo { get; set; }
        public int NoOfProducts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
    }
}
