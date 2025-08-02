using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ModuleMasterErp
    {
        public ModuleMasterErp()
        {
            this.ModuleMasterErpRights = new List<ModuleMasterErpRight>();
        }

        public short ModuleMasterErpID { get; set; }
        public string ModuleMasterErpName { get; set; }
        public string ModuleMasterErpGroupName { get; set; }
        public string ModuleMasterErpPageURL { get; set; }
        public virtual ICollection<ModuleMasterErpRight> ModuleMasterErpRights { get; set; }
    }
}
