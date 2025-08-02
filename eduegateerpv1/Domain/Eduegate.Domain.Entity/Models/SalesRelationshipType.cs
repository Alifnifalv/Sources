using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("SalesRelationshipType", Schema = "catalog")]
    public partial class SalesRelationshipType
    {
        [Key]
        public byte CultureID { get; set; }
        public byte SalesRelationTypeID { get; set; }
        public string RelationName { get; set; }
        public virtual Culture Culture { get; set; }
    }
}
