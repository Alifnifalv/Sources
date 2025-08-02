using System.Runtime.Serialization;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class DocumentTypeSettingsDTO : BaseMasterDTO
    {
        [DataMember]
        public long DocumentTypeSettingID { get; set; }

        [DataMember]
        public int? DocumentTypeID { get; set; }

        [DataMember]
        public string SettingCode { get; set; }

        [DataMember]
        public string SettingValue { get; set; }
    }
}