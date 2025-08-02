using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("Routes", Schema = "distribution")]
    public partial class Route
    {
        public Route()
        {
            this.Areas = new List<Area>();
        }

        [Key]
        public int RouteID { get; set; }
        public Nullable<long> WarehouseID { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual ICollection<Area> Areas { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Country Country { get; set; }
        public int? CostCenterID { get; set; }

        public virtual CostCenter CostCenter { get; set; }
    }
}
