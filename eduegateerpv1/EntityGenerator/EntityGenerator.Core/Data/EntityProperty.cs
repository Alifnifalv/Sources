using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityProperties", Schema = "mutual")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("EntityPropertyTypeID")]
        [InverseProperty("EntityProperties")]
        public virtual EntityPropertyType EntityPropertyType { get; set; }
    }
}
