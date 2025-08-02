using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("ProductPriceLists", Schema = "catalog")]
    public partial class ProductPriceList
    {
        public ProductPriceList()
        {
            ProductPriceListBranchMaps = new HashSet<ProductPriceListBranchMap>();
            ProductPriceListBrandMaps = new HashSet<ProductPriceListBrandMap>();
            ProductPriceListCategoryMaps = new HashSet<ProductPriceListCategoryMap>();
            ProductPriceListCustomerGroupMaps = new HashSet<ProductPriceListCustomerGroupMap>();
            ProductPriceListCustomerMaps = new HashSet<ProductPriceListCustomerMap>();
            ProductPriceListProductMaps = new HashSet<ProductPriceListProductMap>();
            ProductPriceListSKUMaps = new HashSet<ProductPriceListSKUMap>();
        }

        [Key]
        public long ProductPriceListIID { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string PriceDescription { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? Price { get; set; }
        [Column(TypeName = "decimal(18, 3)")]
        public decimal? PricePercentage { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public short? ProductPriceListTypeID { get; set; }
        public short? ProductPriceListLevelID { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public bool? ExcludeCategory { get; set; }
        public bool? IsActive { get; set; }
        [StringLength(20)]
        public string PriceListCode { get; set; }

        [ForeignKey("ProductPriceListLevelID")]
        [InverseProperty("ProductPriceLists")]
        public virtual ProductPriceListLevel ProductPriceListLevel { get; set; }
        [ForeignKey("ProductPriceListTypeID")]
        [InverseProperty("ProductPriceLists")]
        public virtual ProductPriceListType ProductPriceListType { get; set; }
        [InverseProperty("ProductPriceList")]
        public virtual ICollection<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }
        [InverseProperty("ProductPriceList")]
        public virtual ICollection<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }
        [InverseProperty("ProductPriceList")]
        public virtual ICollection<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }
        [InverseProperty("ProductPriceList")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }
        [InverseProperty("ProductPriceList")]
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }
        [InverseProperty("ProductPriceList")]
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }
        [InverseProperty("ProductPriceList")]
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }
    }
}
