using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductFamilyPropertyMaps", Schema = "catalog")]
    public partial class ProductFamilyPropertyMap
    {
        [Key]
        public long ProductFamilyPropertyMapIID { get; set; }
        public long? ProductFamilyID { get; set; }
        public long? ProductPropertyID { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductFamilyID")]
        [InverseProperty("ProductFamilyPropertyMaps")]
        public virtual ProductFamily ProductFamily { get; set; }
        [ForeignKey("ProductPropertyID")]
        [InverseProperty("ProductFamilyPropertyMaps")]
        public virtual Property ProductProperty { get; set; }
    }
}
