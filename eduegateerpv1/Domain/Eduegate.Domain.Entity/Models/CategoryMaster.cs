using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryMaster
    {
        public CategoryMaster()
        {
            this.CategoryColumns = new List<CategoryColumn>();
            this.ProductCategories = new List<ProductCategory>();
            this.DeliveryTypeMasters = new List<DeliveryTypeMaster>();
            this.ProductMasters = new List<ProductMaster>();
        }

        [Key]
        public int CategoryID { get; set; }
        public int ParentID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryNameEn { get; set; }
        public short CategoryPosition { get; set; }
        public byte CategoryLevel { get; set; }
        public string CategoryBreadCrumbs { get; set; }
        public string CategoryLogo { get; set; }
        public string CategoryKeywordsEn { get; set; }
        public string CategoryDescriptionEn { get; set; }
        public bool CategoryActive { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryKeywordsAr { get; set; }
        public string CategoryDescriptionAr { get; set; }
        public string SeoKeywordsAr { get; set; }
        public string SeoDescriptionAr { get; set; }
        public Nullable<bool> CategoryActiveKSA { get; set; }
        public virtual ICollection<CategoryColumn> CategoryColumns { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<DeliveryTypeMaster> DeliveryTypeMasters { get; set; }
        public virtual ICollection<ProductMaster> ProductMasters { get; set; }
    }
}
