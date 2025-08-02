using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CategoryFeeMaps", Schema = "catalog")]
    public partial class CategoryFeeMap
    {
        [Key]
        public long CategoryFeeMapIID { get; set; }
        public long? FeeMasterID { get; set; }
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
