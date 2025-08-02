namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("offlineindex.CategoryTree")]
    public partial class CategoryTree
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CategoryIID { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(50)]
        public string CategoryCode { get; set; }

        public long? ParentCategoryID { get; set; }

        public string CategorySearch { get; set; }

        public string CategoryIDSearch { get; set; }

        public string path { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? x { get; set; }
    }
}
