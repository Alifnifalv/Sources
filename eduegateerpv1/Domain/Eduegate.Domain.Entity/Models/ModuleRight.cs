using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ModuleRight
    {
        [Key]
        public long RefUserID { get; set; }
        public int RefModuleID { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}
