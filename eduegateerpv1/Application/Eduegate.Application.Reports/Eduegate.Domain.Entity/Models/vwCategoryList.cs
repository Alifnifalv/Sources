using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCategoryList
    {
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
        public Nullable<int> SubCategoryCount { get; set; }
        public string CategoryNameAr { get; set; }
        public string CategoryKeywordsAr { get; set; }
        public string CategoryDescriptionAr { get; set; }
    }
}
