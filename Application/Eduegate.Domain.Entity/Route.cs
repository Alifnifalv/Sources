namespace Eduegate.Domain.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("distribution.Routes")]
    public partial class Route
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RouteID { get; set; }

        public long? WarehouseID { get; set; }

        public string Description { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamps { get; set; }

        public int? CompanyID { get; set; }

        public int? CountryID { get; set; }

        public virtual Country Country { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
