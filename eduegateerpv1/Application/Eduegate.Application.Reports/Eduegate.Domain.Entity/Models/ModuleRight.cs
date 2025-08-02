using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ModuleRight
    {
        public long RefUserID { get; set; }
        public int RefModuleID { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}
