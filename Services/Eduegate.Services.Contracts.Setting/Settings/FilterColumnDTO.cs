using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Setting.Settings
{
    [DataContract]
    public class FilterColumnDTO : BaseMasterDTO
    {
        [DataMember]
        public long FilterColumnID { get; set; }

        [DataMember]
        public int? SequenceNo { get; set; }

        [DataMember]
        public long? ViewID { get; set; }

        [DataMember]
        public string ColumnCaption { get; set; }

        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public byte? DataTypeID { get; set; }

        [DataMember]
        public byte? UIControlTypeID { get; set; }

        [DataMember]
        public string DefaultValues { get; set; }

        [DataMember]
        public bool? IsQuickFilter { get; set; }

        [DataMember]
        public int? LookupID { get; set; }

        [DataMember]
        public string Attribute1 { get; set; }

        [DataMember]
        public string Attribute2 { get; set; }

        [DataMember]
        public bool? IsLookupLazyLoad { get; set; }
    }
}