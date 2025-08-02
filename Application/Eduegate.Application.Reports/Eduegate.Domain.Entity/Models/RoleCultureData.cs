using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class RoleCultureData
    {
        public int RoleID { get; set; }
        public byte CultureID { get; set; }
        public string RoleName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual Role Role { get; set; }
    }
}
