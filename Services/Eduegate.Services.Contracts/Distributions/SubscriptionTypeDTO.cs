using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Distributions
{
    [DataContract]
    public class SubscriptionTypeDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public short SubscriptionTypeID { get; set; }

        [DataMember]
        public string SubscriptionName { get; set; }

    }
}