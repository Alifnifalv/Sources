using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Eduegate.Services.Contracts.Search
{
    [DataContract]
    public class ColumnDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string Header { get; set; }
        [DataMember]
        public string DataType { get; set; }
        [DataMember]
        public string Format { get; set; }
        [DataMember]
        public bool IsDefault { get; set; }
        [DataMember]
        public bool IsVisible { get; set; }
        [DataMember]
        public bool IsSortable { get; set; }
        [DataMember]
        public bool IsQuickSearchable { get; set; }
        [DataMember]
        public bool IsExpression { get; set; }
        [DataMember]
        public string Expression { get; set; }
        [DataMember]
        public string FilterValue { get; set; }
    }
}
