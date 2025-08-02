using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Warehouses", Schema = "mutual")]
    public partial class Warehous
    {
        public Warehous()
        {
            Branches = new HashSet<Branch>();
            Routes = new HashSet<Route>();
        }

        [Key]
        public long WarehouseID { get; set; }
        [StringLength(50)]
        public string WarehouseName { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public byte? StatusID { get; set; }
        public int? CompanyID { get; set; }

        [ForeignKey("StatusID")]
        [InverseProperty("Warehous")]
        public virtual WarehouseStatus Status { get; set; }
        [InverseProperty("Warehouse")]
        public virtual ICollection<Branch> Branches { get; set; }
        [InverseProperty("Warehouse")]
        public virtual ICollection<Route> Routes { get; set; }
    }
}
