using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductMasterArabicLog
    {
        [Key]
        public int ProductMasterArabicLog1 { get; set; }
        public Nullable<long> RefUserID { get; set; }
        public Nullable<int> RefProductID { get; set; }
        public string LogKey { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public byte Batch { get; set; }
    }
}
