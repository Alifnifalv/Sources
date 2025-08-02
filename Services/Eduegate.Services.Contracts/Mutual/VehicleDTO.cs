using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class VehicleDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long VehicleID { get; set; }
        [DataMember]
        public Nullable<short> VehicleTypeID { get; set; }
        [DataMember]
        public Nullable<short> VehicleOwnershipTypeID { get; set; }
        [DataMember]
        public string RegistrationName { get; set; }
        [DataMember]
        public string VehicleCode { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string RegistrationNo { get; set; }
        [DataMember]
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> RegistrationExpire { get; set; }
        [DataMember]
        public Nullable<System.DateTime> InsuranceExpire { get; set; }
        [DataMember]
        public Nullable<int> RigistrationCityID { get; set; }
        [DataMember]
        public string CityName { get; set; }
        [DataMember]
        public Nullable<int> RigistrationCountryID { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
