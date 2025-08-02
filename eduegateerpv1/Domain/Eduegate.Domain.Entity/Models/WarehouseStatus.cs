using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("WarehouseStatuses", Schema = "mutual")]
    public partial class WarehouseStatus
    {
        public WarehouseStatus() 
        {
            this.Warehouses = new List<Warehouse>();
        }

        [Key]
        public byte WarehouseStatusID { get; set; }  
        public string StatusName { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; } 
    }
}
