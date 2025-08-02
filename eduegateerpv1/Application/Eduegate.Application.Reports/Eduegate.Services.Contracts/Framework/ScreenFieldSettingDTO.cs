using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Framework
{
    [DataContract]
    public class ScreenFieldSettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ScreenFieldSettingID { get; set; }
        [DataMember]
        public long ScreenFieldID { get; set; }
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
