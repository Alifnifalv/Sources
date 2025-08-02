using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductSearchKeywordsIntl
    {
        public long LogID { get; set; }
        public string Keyword { get; set; }
        public byte Preference { get; set; }
        public short RefCountryID { get; set; }
    }
}
