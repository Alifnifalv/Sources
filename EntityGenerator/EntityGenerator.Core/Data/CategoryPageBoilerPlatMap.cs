using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("CategoryPageBoilerPlatMaps", Schema = "cms")]
    public partial class CategoryPageBoilerPlatMap
    {
        [Key]
        public long CategoryPageBoilerPlatMapIID { get; set; }
        public long? CategoryID { get; set; }
        public long? PageBoilerplateMapID { get; set; }
        public int? SerialNumber { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }

        [ForeignKey("PageBoilerplateMapID")]
        [InverseProperty("CategoryPageBoilerPlatMaps")]
        public virtual PageBoilerplateMap PageBoilerplateMap { get; set; }
    }
}
