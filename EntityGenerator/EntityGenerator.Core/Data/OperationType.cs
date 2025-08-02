using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("OperationTypes", Schema = "sync")]
    public partial class OperationType
    {
        public OperationType()
        {
            EntityChangeTrackers = new HashSet<EntityChangeTracker>();
        }

        [Key]
        public int OperationTypeID { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string OperationName { get; set; }

        [InverseProperty("OperationType")]
        public virtual ICollection<EntityChangeTracker> EntityChangeTrackers { get; set; }
    }
}
