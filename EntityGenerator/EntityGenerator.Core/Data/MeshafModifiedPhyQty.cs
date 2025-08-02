using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Keyless]
    [Table("MeshafModifiedPhyQty")]
    public partial class MeshafModifiedPhyQty
    {
        public double? ProductIID { get; set; }
        public double? ProductSKUMapID { get; set; }
        [StringLength(255)]
        public string ProductSKUCode { get; set; }
        [StringLength(255)]
        public string ProductName { get; set; }
        public double? Physical_Stock { get; set; }
        public double? NewPhyQty { get; set; }
        [StringLength(255)]
        public string F7 { get; set; }
        [StringLength(255)]
        public string F8 { get; set; }
    }
}
