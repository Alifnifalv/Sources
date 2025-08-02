using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class UserMaster
    {
        public UserMaster()
        {
            this.ModuleMasterErpRights = new List<ModuleMasterErpRight>();
            this.ModuleRights = new List<ModuleRight>();
            this.UserLogs = new List<UserLog>();
            this.VoucherMasterDetails = new List<VoucherMasterDetail>();
        }

        public long UserID { get; set; }
        public string LoginID { get; set; }
        public string LoginPwd { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool Superuser { get; set; }
        public bool Active { get; set; }
        public string Dept { get; set; }
        public string SessionID { get; set; }
        public Nullable<bool> IsDriver { get; set; }
        public Nullable<bool> IsVendor { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> IsRestricted { get; set; }
        public virtual ICollection<ModuleMasterErpRight> ModuleMasterErpRights { get; set; }
        public virtual ICollection<ModuleRight> ModuleRights { get; set; }
        public virtual ICollection<UserLog> UserLogs { get; set; }
        public virtual ICollection<VoucherMasterDetail> VoucherMasterDetails { get; set; }
    }
}
