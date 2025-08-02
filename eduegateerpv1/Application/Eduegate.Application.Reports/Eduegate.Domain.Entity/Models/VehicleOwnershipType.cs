using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VehicleOwnershipType
    {
        public VehicleOwnershipType()
        {
            this.Vehicles = new List<Vehicle>();
        }

        public short VehicleOwnershipTypeID { get; set; }
        public string OwnershipTypeName { get; set; }
        public string Description { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
