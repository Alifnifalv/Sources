using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CategoryTagMaps", Schema = "catalog")]
    public partial class CategoryTagMap
    {
        [Key]
        public long CategoryTagMapIID { get; set; }
        public long? CategoryID { get; set; }
        public long? CategoryTagID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("CategoryID")]
        [InverseProperty("CategoryTagMaps")]
        public virtual Category Category { get; set; }
        [ForeignKey("CategoryTagID")]
        [InverseProperty("CategoryTagMaps")]
        public virtual CategoryTag CategoryTag { get; set; }
    }
}
