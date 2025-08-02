using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Metadata
{
    [DataContract]
    public class FilterColumnDTO
    {
        [DataMember]
        public long FilterColumnID {get;set;}
        [DataMember]
        public int SequenceNo { get; set; }
        [DataMember]
        public string ColumnCaption { get; set; }
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.DataTypes ColumnType { get; set; }
        [DataMember]
        public string DefaultValues { get; set; }
        [DataMember]
        public bool IsQuickFilter { get; set; }
        [DataMember]
        public Nullable<int> LookUpID { get; set; }
        [DataMember]
        public bool IsLookupLazyLoad { get; set; }
        [DataMember]
        public List<Enums.Conditions> FilterConditions { get; set; }
        [DataMember]
        public Enums.UIControlTypes FilterControlType { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public string Attribute1 { get; set; }
        [DataMember]
        public string Attribute2 { get; set; }
    }
}
