using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BrandMaster
    {
        public int BrandID { get; set; }
        public string BrandCode { get; set; }
        public string BrandNameEn { get; set; }
        public short BrandPosition { get; set; }
        public string BrandLogo { get; set; }
        public string BrandKeywordsEn { get; set; }
        public string BrandDescriptionEn { get; set; }
        public bool BrandActive { get; set; }
        public Nullable<bool> BrandDisplayHomePage { get; set; }
        public string BrandNameAr { get; set; }
        public string BrandKeywordsAr { get; set; }
        public string BrandDescriptionAr { get; set; }
        public bool IsBranded { get; set; }
    }
}
