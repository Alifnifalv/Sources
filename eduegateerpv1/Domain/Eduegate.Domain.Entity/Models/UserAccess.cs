using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserAccess
    {
        [Key]
        public int UserAccessID { get; set; }
        public long RefUserID { get; set; }
        public string AccessValues { get; set; }
        public int CreatedByID { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}
