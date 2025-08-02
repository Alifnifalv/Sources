using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class WarehouseStatus
    {
        public WarehouseStatus()
        {
            this.Warehouses = new List<Warehouse>();
        }

        public byte WarehouseStatusID { get; set; }
        public string StatusName { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
