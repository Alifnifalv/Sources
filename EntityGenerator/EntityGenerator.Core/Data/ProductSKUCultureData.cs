using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductSKUCultureDatas", Schema = "catalog")]
    public partial class ProductSKUCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long ProductSKUMapID { get; set; }
        [StringLength(1000)]
        public string ProductSKUName { get; set; }
        public string ProductSKUDescription { get; set; }
        public string ProductSKUDetails { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ProductSKUCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ProductSKUMapID")]
        [InverseProperty("ProductSKUCultureDatas")]
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
