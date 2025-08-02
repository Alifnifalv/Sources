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
    public class ZoneDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public short ZoneID { get; set; }
        [DataMember]
        public string ZoneName { get; set; }
        [DataMember]
        public Nullable<int> CountryID { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
    }
}
