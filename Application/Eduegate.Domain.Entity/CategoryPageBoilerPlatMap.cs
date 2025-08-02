namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cms.CategoryPageBoilerPlatMaps")]
    public partial class CategoryPageBoilerPlatMap
    {
        [Key]
        public long CategoryPageBoilerPlatMapIID { get; set; }

        public long? CategoryID { get; set; }

        public long? PageBoilerplateMapID { get; set; }

        public int? SerialNumber { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual PageBoilerplateMap PageBoilerplateMap { get; set; }
    }
}
