using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class VehicleSearchView
    {
        public long VehicleIID { get; set; }
        public string VehicleCode { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<System.DateTime> RegistrationExpire { get; set; }
        public Nullable<System.DateTime> InsuranceExpire { get; set; }
        public Nullable<int> CompanyID { get; set; }
    }
}
