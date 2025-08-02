using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class ProductClass_20230505
    {
        [StringLength(255)]
        public string ClassData { get; set; }
        [Column("SKU Name")]
        [StringLength(255)]
        public string SKU_Name { get; set; }
        public double? ProductSKUMapIID { get; set; }
        public double? ProductID { get; set; }
        [Column(TypeName = "money")]
        public decimal? ProductPrice { get; set; }
        public int? OptionalSubjectID { get; set; }
        public int? SecondLangID { get; set; }
        public int? ThirdLangID { get; set; }
    }
}
