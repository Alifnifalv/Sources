using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Settings
{
    [DataContract]
    public class SettingDTO
    {
        [DataMember]
        public string GroupName { get; set; }

        [DataMember]
        public int? CompanyID { get; set; }

        [DataMember]
        public int? SiteID { get; set; }

        [DataMember]
        public string SettingCode { get; set; }

        [DataMember]
        public string SettingValue { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string ValueType { get; set; }

        [DataMember]
        public int? LookupTypeID { get; set; }
    }
}
