using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("OperationTypes", Schema = "sync")]
    public partial class OperationType
    {
        public OperationType()
        {
            this.EntityChangeTrackers = new List<EntityChangeTracker>();
        }

        [Key]
        public int OperationTypeID { get; set; }
        public string OperationName { get; set; }
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
