using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PermissionCultureData
    {
        public int PermissionID { get; set; }
        public byte CultureID { get; set; }
        public string PermissionName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Culture Culture { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
