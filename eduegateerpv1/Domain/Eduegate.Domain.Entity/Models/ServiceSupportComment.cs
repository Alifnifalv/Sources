using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ServiceSupportComment
    {
        [Key]
        public int CommentID { get; set; }
        public Nullable<int> RefTicketID { get; set; }
        public Nullable<short> RefUserID { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
