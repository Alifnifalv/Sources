using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VehicleMaster
    {
        [Key]
        public int VehicleMasterID { get; set; }
        public string VehicleNo { get; set; }
        public string Brand { get; set; }
        public System.DateTime DatePurchased { get; set; }
        public string OwnershipHistory { get; set; }
        public string Registration { get; set; }
        public Nullable<long> OdometerReading { get; set; }
        public string InsuranceType { get; set; }
        public Nullable<System.DateTime> DateOfExpiry { get; set; }
        public Nullable<long> Mileage { get; set; }
        public string Warranty { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedTimeStamp { get; set; }
        public Nullable<long> EditedBy { get; set; }
        public Nullable<System.DateTime> EditedTimeStanp { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
