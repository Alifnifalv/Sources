using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserViewColumnMap
    {
        public long UserViewColumnMapIID { get; set; }
        public Nullable<long> UserViewID { get; set; }
        public Nullable<long> ViewColumnID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual UserView UserView { get; set; }
    }
}
