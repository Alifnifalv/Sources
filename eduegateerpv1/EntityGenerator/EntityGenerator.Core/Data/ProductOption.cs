using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductOptions", Schema = "catalog")]
    public partial class ProductOption
    {
        public ProductOption()
        {
            ProductOptionCultureDatas = new HashSet<ProductOptionCultureData>();
        }

        [Key]
        public int ProductOptionID { get; set; }
        [StringLength(100)]
        public string ProductOptionName { get; set; }

        [InverseProperty("ProductOption")]
        public virtual ICollection<ProductOptionCultureData> ProductOptionCultureDatas { get; set; }
    }
}
