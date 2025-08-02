using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryBlock
    {
        public int BlockID { get; set; }
        public long RefCategoryID { get; set; }
        public string BlockTitle { get; set; }
        public string BlockLink { get; set; }
        public string BlockType { get; set; }
        public Nullable<byte> Position { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> RefUserID { get; set; }
        public string BlockTitleAr { get; set; }
        public string BlockLinkAr { get; set; }
        public Nullable<short> RefCountryID { get; set; }
    }
}
