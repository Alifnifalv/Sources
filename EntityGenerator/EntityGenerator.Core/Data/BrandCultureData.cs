using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("BrandCultureDatas", Schema = "catalog")]
    public partial class BrandCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long BrandID { get; set; }
        [StringLength(50)]
        public string BrandName { get; set; }
        [StringLength(1000)]
        public string Descirption { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("BrandID")]
        [InverseProperty("BrandCultureDatas")]
        public virtual Brand Brand { get; set; }
        [ForeignKey("CultureID")]
        [InverseProperty("BrandCultureDatas")]
        public virtual Culture Culture { get; set; }
    }
}
