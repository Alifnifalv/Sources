namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mutual.EntityProperties")]
    public partial class EntityProperty
    {
        [Key]
        public long EntityPropertyIID { get; set; }

        public int? EntityPropertyTypeID { get; set; }

        [StringLength(50)]
        public string PropertyName { get; set; }

        [StringLength(100)]
        public string PropertyDescription { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual EntityPropertyType EntityPropertyType { get; set; }
    }
}
