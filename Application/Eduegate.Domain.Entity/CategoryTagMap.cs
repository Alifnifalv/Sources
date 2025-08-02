namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.CategoryTagMaps")]
    public partial class CategoryTagMap
    {
        [Key]
        public long CategoryTagMapIID { get; set; }

        public long? CategoryID { get; set; }

        public long? CategoryTagID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Category Category { get; set; }

        public virtual CategoryTag CategoryTag { get; set; }
    }
}
