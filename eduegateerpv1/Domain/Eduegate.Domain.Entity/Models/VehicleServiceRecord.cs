using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VehicleServiceRecord
    {
        [Key]
        public int VehicleServiceRecordID { get; set; }
        public Nullable<int> RefVehicleMasterID { get; set; }
        public string Services { get; set; }
        public Nullable<long> RefUserID { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
