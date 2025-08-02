namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductStatusCultureDatas")]
    public partial class ProductStatusCultureData
    {
        [Key]
        [Column(Order = 0)]
        public byte CoultureID { get; set; }

        [Key]
        [Column(Order = 1)]
        public byte ProductStatusID { get; set; }

        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
