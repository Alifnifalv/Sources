using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class RouteDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public int RouteID { get; set; }
        [DataMember]
        public Nullable<int> AreaID { get; set; }
        [DataMember]
        public Nullable<int> CountryID { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<long> WarehouseID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
