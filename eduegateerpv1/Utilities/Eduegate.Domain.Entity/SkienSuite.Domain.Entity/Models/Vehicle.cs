using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            this.JobEntryHeads = new List<JobEntryHead>();
        }

        public long VehicleIID { get; set; }
        public Nullable<short> VehicleTypeID { get; set; }
        public Nullable<short> VehicleOwnershipTypeID { get; set; }
        public string RegistrationName { get; set; }
        public string VehicleCode { get; set; }
        public string Description { get; set; }
        public string RegistrationNo { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public Nullable<System.DateTime> RegistrationExpire { get; set; }
        public Nullable<System.DateTime> InsuranceExpire { get; set; }
        public Nullable<int> RigistrationCityID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> RigistrationCountryID { get; set; }
        public virtual ICollection<JobEntryHead> JobEntryHeads { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual VehicleOwnershipType VehicleOwnershipType { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}
