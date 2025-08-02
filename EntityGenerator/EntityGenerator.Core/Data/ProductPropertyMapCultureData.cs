using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPropertyMapCultureDatas", Schema = "catalog")]
    public partial class ProductPropertyMapCultureData
    {
        [Key]
        public byte CultureID { get; set; }
        [Key]
        public long ProductPropertyMapID { get; set; }
        [StringLength(50)]
        public string Value { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
    }
}
