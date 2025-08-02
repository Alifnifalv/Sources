namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.BrandTagMaps")]
    public partial class BrandTagMap
    {
        [Key]
        public long BrandTagMapIID { get; set; }

        public long? BrandTagID { get; set; }

        public long? BrandID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual BrandTag BrandTag { get; set; }
    }
}
