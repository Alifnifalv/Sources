namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.Products_20230222")]
    public partial class Products_20230222
    {
        [Key]
        [Column(Order = 0)]
        public long ProductIID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string ProductCode { get; set; }

        public long? ProductTypeID { get; set; }

        public long? ProductFamilyID { get; set; }

        public long? BrandID { get; set; }

        [StringLength(1000)]
        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public byte? StatusID { get; set; }

        public long? SeoMetadataIID { get; set; }

        public decimal? Weight { get; set; }

        public long? ManufactureID { get; set; }

        public long? ManufactureCountryID { get; set; }

        public long? UnitGroupID { get; set; }

        public long? UnitID { get; set; }

        public bool? IsMultipleSKUEnabled { get; set; }

        public bool? IsOnline { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdateBy { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public long? ProductOwnderID { get; set; }

        public int? TaxTemplateID { get; set; }

        public long? BaseUnitID { get; set; }

        public bool? IsWeighingProduct { get; set; }

        public long? PurchaseUnitGroupID { get; set; }

        public long? SellingUnitGroupID { get; set; }

        public long? PurchaseUnitID { get; set; }

        public long? SellingUnitID { get; set; }
    }
}
