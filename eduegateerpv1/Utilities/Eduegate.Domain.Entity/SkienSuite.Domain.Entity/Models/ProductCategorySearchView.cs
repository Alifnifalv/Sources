using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductCategorySearchView
    {
        public string RowCategory { get; set; }
        public Nullable<long> ParentCategoryID { get; set; }
        public long CategoryIID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string ImageName { get; set; }
        public string ThumbnailImageName { get; set; }
        public Nullable<long> SeoMetadataID { get; set; }
        public Nullable<bool> IsInNavigationMenu { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Updated { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
