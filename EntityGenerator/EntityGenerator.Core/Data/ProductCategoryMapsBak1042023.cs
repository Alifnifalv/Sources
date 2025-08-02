using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("ProductCategoryMapsBak1042023", Schema = "catalog")]
    public partial class ProductCategoryMapsBak1042023
    {
        public long ProductCategoryMapIID { get; set; }
        public long? ProductID { get; set; }
        public long? CategoryID { get; set; }
        public bool? IsPrimary { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
