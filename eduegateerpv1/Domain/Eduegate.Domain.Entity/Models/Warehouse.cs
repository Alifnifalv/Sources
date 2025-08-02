using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Warehouses", Schema = "mutual")]
    public partial class Warehouse
    {
        public Warehouse()
        {
            this.Routes = new List<Route>();
            this.Branches = new List<Branch>();
        }

        [Key]
        public long WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public Nullable<byte> StatusID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual WarehouseStatus WarehouseStatuses { get; set; }
    }
}
