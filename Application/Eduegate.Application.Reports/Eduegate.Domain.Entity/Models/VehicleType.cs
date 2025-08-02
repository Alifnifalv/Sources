using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            this.Vehicles = new List<Vehicle>();
        }

        public short VehicleTypeID { get; set; }
        public string VehicleTypeName { get; set; }
        public string Capacity { get; set; }
        public string Dimensions { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
