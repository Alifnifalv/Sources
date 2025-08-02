namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.SalesRelationshipType")]
    public partial class SalesRelationshipType
    {
        [Key]
        [Column(Order = 0)]
        public byte CultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte SalesRelationTypeID { get; set; }

        [StringLength(50)]
        public string RelationName { get; set; }

        public virtual Culture Culture { get; set; }
    }
}
