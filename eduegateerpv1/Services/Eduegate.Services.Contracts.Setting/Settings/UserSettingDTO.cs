using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Setting.Settings
{
    [DataContract]
    public class SettingDTO : BaseMasterDTO
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