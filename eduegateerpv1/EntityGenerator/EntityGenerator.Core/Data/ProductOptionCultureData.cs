using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductOptionCultureDatas", Schema = "catalog")]
    public partial class ProductOptionCultureData
    {
        [Key]
        public int ProductOptionID { get; set; }
        [Key]
        public byte CultureID { get; set; }
        [StringLength(100)]
        public string ProductOptionName { get; set; }

        [ForeignKey("CultureID")]
        [InverseProperty("ProductOptionCultureDatas")]
        public virtual Culture Culture { get; set; }
        [ForeignKey("ProductOptionID")]
        [InverseProperty("ProductOptionCultureDatas")]
        public virtual ProductOption ProductOption { get; set; }
    }
}
