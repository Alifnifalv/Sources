using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public partial class DataFeedLogDTO
    {
        [DataMember]
        public long DataFeedLogID { get; set; }
        [DataMember]
        public int DataFeedTypeID { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.DataFeedTypes DataFeedType { get; set; }
        [DataMember]
        public Eduegate.Services.Contracts.Enums.DataFeedStatus DataFeedStatusID { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public string DataFeedTypeName { get; set; }
        [DataMember]
        public string DataFeedStatusName { get; set; }
    }
}
