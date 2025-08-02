using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductCultureDatas", Schema = "catalog")]
    public partial class ProductCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long ProductID { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDetails { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ProductCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ProductID")]
        [InverseProperty("ProductCultureDatas")]
        public virtual Product Product { get; set; }
    }
}
