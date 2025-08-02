using System.Runtime.Serialization;
using System.ServiceModel;

namespace Eduegate.Services.Contracts.Enums
{
    [DataContract(Name = "StaticContentTypes")]
    public enum StaticContentTypes
    {
        [EnumMember]
        ByBrand = 1,
        [EnumMember]
        ByEduegates = 2,
        [EnumMember]
        All = 0,
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    [DataContract(Name = "EmailNotificationType")]
    public class StaticContentType
    {
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "ByBrand")]
        public class ByBrand
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string ProductID = "ProductID";

                [DataMember]
                public const string SKUID = "SKUID";
            }
        }

        [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
        [DataContract(Name = "ByEduegates")]
        public class ByEduegates
        {
            [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string BrandID = "BrandID";
            }
        }
    }
}
