using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ScreenLookupMaps", Schema = "setting")]
    public partial class ScreenLookupMap
    {
        [Key]
        public long ScreenLookupMapID { get; set; }
        public Nullable<long> ScreenID { get; set; }
        public Nullable<bool> IsOnInit { get; set; }
        public string LookUpName { get; set; }
        public string Url { get; set; }
        public string CallBack { get; set; }
        public virtual ScreenMetadata ScreenMetadata { get; set; }
    }
}
