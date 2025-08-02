using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSearchKeywordsAr 
    {
        public long LogID { get; set; }
        public string KeywordAr { get; set; }
        public byte Preference { get; set; }
    }
}
