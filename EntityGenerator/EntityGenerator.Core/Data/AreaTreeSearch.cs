using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    public partial class AreaTreeSearch
    {
        public int AreaID { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        public int? ParentAreaID { get; set; }
        [Unicode(false)]
        public string TreePath { get; set; }
    }
}
