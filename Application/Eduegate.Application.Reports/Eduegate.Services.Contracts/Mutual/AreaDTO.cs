using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class AreaDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public Nullable<int> AreaID { get; set; }
        [DataMember]
        public string AreaName { get; set; }
        [DataMember]
        public Nullable<int> RouteID { get; set; }
        [DataMember]
        public Nullable<short> ZoneID { get; set; }
        [DataMember]
        public Nullable<int> CityID { get; set; }        
        [DataMember]
        public String CityName { get; set; }
        [DataMember]
        public Nullable<int> CountryID { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public Nullable<bool> IsActive { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
