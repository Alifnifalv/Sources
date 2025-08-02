using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserViewSummaryColumnMap
    {
        [Key]
        public long UserViewSummaryColumnMapIID { get; set; }
        public long UserViewID { get; set; }
        public long ViewSummaryColumnID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual UserView UserView { get; set; }
    }
}
