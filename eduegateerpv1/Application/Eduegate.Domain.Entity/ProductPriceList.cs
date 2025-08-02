namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("catalog.ProductPriceLists")]
    public partial class ProductPriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
        public string PriceDescription { get; set; }

        public decimal? Price { get; set; }

        public decimal? PricePercentage { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public short? ProductPriceListTypeID { get; set; }

        public short? ProductPriceListLevelID { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public bool? ExcludeCategory { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(20)]
        public string PriceListCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListBranchMap> ProductPriceListBranchMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListBrandMap> ProductPriceListBrandMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCategoryMap> ProductPriceListCategoryMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerGroupMap> ProductPriceListCustomerGroupMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListCustomerMap> ProductPriceListCustomerMaps { get; set; }

        public virtual ProductPriceListLevel ProductPriceListLevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListProductMap> ProductPriceListProductMaps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductPriceListSKUMap> ProductPriceListSKUMaps { get; set; }

        public virtual ProductPriceListType ProductPriceListType { get; set; }
    }
}
