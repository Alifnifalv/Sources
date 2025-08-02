using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserViewFilterMap
    {
        [Key]
        public long UserViewFilterMapIID { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<long> ViewFilterID { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Login Login { get; set; }
        public virtual ViewFilter ViewFilter { get; set; }
    }
}
