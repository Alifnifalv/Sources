using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ModuleMasterErpRight
    {
        [Key]
        public int ModuleMasterErpRightsID { get; set; }
        public short RefModuleMasterErpID { get; set; }
        public long RefUserID { get; set; }
        public long RefCountryID { get; set; }
        public virtual ModuleMasterErp ModuleMasterErp { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}
