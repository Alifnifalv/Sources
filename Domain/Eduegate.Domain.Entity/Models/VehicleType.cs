using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("VehicleTypes", Schema = "mutual")]
    public partial class VehicleType
    {
        public VehicleType()
        {
            this.Vehicles = new List<Vehicle>();
        }

        [Key]
        public short VehicleTypeID { get; set; }
        public string VehicleTypeName { get; set; }
        public string Capacity { get; set; }
        public string Dimensions { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
