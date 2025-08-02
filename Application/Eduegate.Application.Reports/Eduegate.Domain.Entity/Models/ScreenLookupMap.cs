using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ScreenLookupMap
    {
        public long ScreenLookupMapID { get; set; }
        public Nullable<long> ScreenID { get; set; }
        public Nullable<bool> IsOnInit { get; set; }
        public string LookUpName { get; set; }
        public string Url { get; set; }
        public string CallBack { get; set; }
        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}
