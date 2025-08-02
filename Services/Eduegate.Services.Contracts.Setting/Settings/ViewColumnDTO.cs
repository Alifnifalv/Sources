using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Setting.Settings
{
    [DataContract]
    public class ViewColumnDTO : BaseMasterDTO
    {
        [DataMember]
        public long ViewColumnID { get; set; }

        [DataMember]
        public long? ViewID { get; set; }

        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public string DataType { get; set; }

        [DataMember]
        public string PhysicalColumnName { get; set; }

        [DataMember]
        public bool? IsDefault { get; set; }

        [DataMember]
        public bool? IsVisible { get; set; }

        [DataMember]
        public bool? IsSortable { get; set; }

        [DataMember]
        public bool? IsQuickSearchable { get; set; }

        [DataMember]
        public int? SortOrder { get; set; }

        [DataMember]
        public bool? IsExpression { get; set; }

        [DataMember]
        public string Expression { get; set; }

        [DataMember]
        public string FilterValue { get; set; }
    }
}