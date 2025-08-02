using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Supports.Models.Catalogs
{
    [Table("ProductSKUMaps", Schema = "catalog")]
    public partial class ProductSKUMap
    {
        public ProductSKUMap()
        {
            TicketProductMaps = new HashSet<TicketProductMap>();
        }

        [Key]
        public long ProductSKUMapIID { get; set; }

        public long? ProductID { get; set; }

        public int? Sequence { get; set; }

        [StringLength(150)]
        public string ProductSKUCode { get; set; }

        [StringLength(50)]
        public string PartNo { get; set; }

        [StringLength(50)]
        public string BarCode { get; set; }

        public decimal? ProductPrice { get; set; }

        [StringLength(200)]
        public string VariantsMap { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        //public byte[] TimeStamps { get; set; }

        public bool? IsHiddenFromList { get; set; }

        public bool? HideSKU { get; set; }

        [StringLength(1000)]
        public string SKUName { get; set; }

        public byte? StatusID { get; set; }

        public long? SeoMetadataID { get; set; }

        [StringLength(20)]
        public string ProductSKUMapIIDTEXT { get; set; }

        public int? CompanyID { get; set; }

        [StringLength(50)]
        public string ExternalCode { get; set; }

        public decimal? ProductNos { get; set; }

        public string WarningMessage { get; set; }

        public bool? IsOutOfStock { get; set; }

        public bool? IsBulk { get; set; }

        public int? ProductOptionGroupID { get; set; }

        [StringLength(50)]
        public string ProductNosText { get; set; }

        [StringLength(1000)]
        public string RelevenceSortName { get; set; }

        public string TooltipMessage { get; set; }

        public bool? IsActive { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<TicketProductMap> TicketProductMaps { get; set; }
    }
}