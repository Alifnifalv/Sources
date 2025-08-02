using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ModuleMaster
    {
        [Key]
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string GroupName { get; set; }
        public string PageURL { get; set; }
    }
}
