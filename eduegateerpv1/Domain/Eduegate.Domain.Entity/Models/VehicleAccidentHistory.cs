using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VehicleAccidentHistory
    {
        [Key]
        public int VehicleAccidentHistoryID { get; set; }
        public Nullable<int> RefVehicleMasterID { get; set; }
        public string Accidents { get; set; }
        public Nullable<long> RefUserID { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}
