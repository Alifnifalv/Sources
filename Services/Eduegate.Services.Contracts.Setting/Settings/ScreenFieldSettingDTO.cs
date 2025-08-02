using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Setting.Settings
{
    [DataContract]
    public class ScreenFieldSettingDTO : BaseMasterDTO
    {
        [DataMember]
        public long ScreenFieldSettingID { get; set; }

        [DataMember]
        public long? ScreenID { get; set; }

        [DataMember]
        public int? SequenceID { get; set; }

        [DataMember]
        public long? ScreenFieldID { get; set; }

        [DataMember]
        public string FieldName { get; set; }

        [DataMember]
        public string ModelName { get; set; }

        [DataMember]
        public string LookupName { get; set; }

        [DataMember]
        public string DateType { get; set; }

        [DataMember]
        public string DefaultValue { get; set; }

        [DataMember]
        public string DefaultFormat { get; set; }

        [DataMember]
        public string Prefix { get; set; }

        [DataMember]
        public byte? TextTransformTypeId { get; set; }
    }
}