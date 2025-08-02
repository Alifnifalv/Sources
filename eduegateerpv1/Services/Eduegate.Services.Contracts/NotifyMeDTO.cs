using Eduegate.Framework.Contracts.Common;
using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class NotifyMeDTO : BaseMasterDTO
    {
        [DataMember]
        public long NotifyIID { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

        [DataMember]
        public string EmailID { get; set; }
        [DataMember]
        public string CompanyID { get; set; }

        [DataMember]
        public Nullable<int> SiteID { get; set; }
        [DataMember]
        public Nullable<bool> IsEmailSend { get; set; }

        [DataMember]
        public short StatusID { get; set; }

        [DataMember]
        public string StatusMessage { get; set; }
    }
}