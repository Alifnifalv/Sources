using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserView
    {
        public UserView()
        {
            this.UserViewColumnMaps = new List<UserViewColumnMap>();
        }

        public long UserViewIID { get; set; }
        public string UserViewName { get; set; }
        public Nullable<long> LoginID { get; set; }
        public Nullable<long> ViewID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Login Login { get; set; }
        public virtual ICollection<UserViewColumnMap> UserViewColumnMaps { get; set; }
        public virtual View View { get; set; }
    }
}
