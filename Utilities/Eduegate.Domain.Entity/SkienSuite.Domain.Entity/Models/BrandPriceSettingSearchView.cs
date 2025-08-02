using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class BrandPriceSettingSearchView
    {
        public long BrandIID { get; set; }
        public string BrandName { get; set; }
        public string LogoFile { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public string StatusName { get; set; }
        public string Descirption { get; set; }
        public string RowCategory { get; set; }
    }
}
