namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceListBranchMaps")]
    public partial class ProductPriceListBranchMap
    {
        [Key]
        public long ProductPriceListBranchMapIID { get; set; }

        public long? ProductPriceListID { get; set; }

        public long? BranchID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public virtual Branch Branch { get; set; }

        public virtual ProductPriceList ProductPriceList { get; set; }
    }
}
