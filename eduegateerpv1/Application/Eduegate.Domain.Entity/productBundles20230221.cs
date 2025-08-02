namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.productBundles20230221")]
    public partial class productBundles20230221
    {
        [Key]
        public long BundleIID { get; set; }

        public long? FromProductID { get; set; }

        public long? ToProductID { get; set; }

        public long? FromProductSKUMapID { get; set; }

        public long? ToProductSKUMapID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public decimal? Quantity { get; set; }

        public decimal? SellingPrice { get; set; }

        public decimal? CostPrice { get; set; }

        [StringLength(250)]
        public string Remarks { get; set; }
    }
}
