namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.EntityPropertyMaps")]
    public partial class EntityPropertyMap
    {
        [Key]
        public long EntityPropertyMapIID { get; set; }

        public short? EntityTypeID { get; set; }

        public int? EntityPropertyTypeID { get; set; }

        public long? EntityPropertyID { get; set; }

        public long? ReferenceID { get; set; }

        public short? Sequence { get; set; }

        [StringLength(250)]
        public string Value1 { get; set; }

        [StringLength(250)]
        public string Value2 { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }
    }
}
