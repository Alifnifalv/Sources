namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.EntityTypeRelationMaps")]
    public partial class EntityTypeRelationMap
    {
        [Key]
        public long EntityTypeRelationMapsIID { get; set; }

        public int? FromEntityTypeID { get; set; }

        public int? ToEntityTypeID { get; set; }

        public long? FromRelationID { get; set; }

        public long? ToRelationID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual EntityType EntityType { get; set; }

        public virtual EntityType EntityType1 { get; set; }
    }
}
