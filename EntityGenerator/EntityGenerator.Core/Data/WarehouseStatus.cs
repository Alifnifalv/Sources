using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("WarehouseStatuses", Schema = "mutual")]
    public partial class WarehouseStatus
    {
        public WarehouseStatus()
        {
            Warehous = new HashSet<Warehous>();
        }

        [Key]
        public byte WarehouseStatusID { get; set; }
        [StringLength(50)]
        public string StatusName { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Warehous> Warehous { get; set; }
    }
}
