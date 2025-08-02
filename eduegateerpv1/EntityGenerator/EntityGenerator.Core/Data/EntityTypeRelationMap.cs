using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityTypeRelationMaps", Schema = "mutual")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("FromEntityTypeID")]
        [InverseProperty("EntityTypeRelationMapFromEntityTypes")]
        public virtual EntityType FromEntityType { get; set; }
        [ForeignKey("ToEntityTypeID")]
        [InverseProperty("EntityTypeRelationMapToEntityTypes")]
        public virtual EntityType ToEntityType { get; set; }
    }
}
