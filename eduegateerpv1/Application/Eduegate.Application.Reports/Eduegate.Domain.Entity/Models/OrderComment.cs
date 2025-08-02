using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class OrderComment
    {
        public int OrderCommentID { get; set; }
        public Nullable<int> RefOrderID { get; set; }
        public Nullable<long> RefUserID { get; set; }
        public string OrderComment1 { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
