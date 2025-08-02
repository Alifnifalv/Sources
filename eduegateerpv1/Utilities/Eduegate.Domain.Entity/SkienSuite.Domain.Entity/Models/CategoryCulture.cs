using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryCulture
    {
        public long CategoryIID { get; set; }
        public Nullable<long> ParentCategoryID { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string ImageName { get; set; }
        public string ThumbnailImageName { get; set; }
        public Nullable<long> SeoMetadataID { get; set; }
        public Nullable<bool> IsInNavigationMenu { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<byte> CultureID { get; set; }
    }
}
