using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("EntityPropertyMaps", Schema = "mutual")]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
