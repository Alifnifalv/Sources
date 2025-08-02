using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductFamilyPropertyTypeMaps", Schema = "catalog")]
    public partial class ProductFamilyPropertyTypeMap
    {
        [Key]
        public long ProductFamilyPropertyTypeMapIID { get; set; }
        public long? ProductFamilyID { get; set; }
        public byte? PropertyTypeID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("ProductFamilyID")]
        [InverseProperty("ProductFamilyPropertyTypeMaps")]
        public virtual ProductFamily ProductFamily { get; set; }
    }
}
