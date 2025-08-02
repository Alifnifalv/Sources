using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityGenerator.Core.Data
{
    [Table("Routes", Schema = "distribution")]
    public partial class Route
    {
        [Key]
        public int RouteID { get; set; }
        public long? WarehouseID { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public int? CompanyID { get; set; }
        public int? CountryID { get; set; }

        [ForeignKey("CountryID")]
        [InverseProperty("Routes")]
        public virtual Country Country { get; set; }
        [ForeignKey("WarehouseID")]
        [InverseProperty("Routes")]
        public virtual Warehous Warehouse { get; set; }
    }
}
