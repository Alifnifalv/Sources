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

    [DataContract(Name = "EmailNotificationType")]
    public class StaticContentType
    {
        [DataContract(Name = "ByBrand")]
        public class ByBrand
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string ProductID = "ProductID";

                [DataMember]
                public const string SKUID = "SKUID";
            }
        }

        [DataContract(Name = "ByEduegates")]
        public class ByEduegates
        {
            [DataContract(Name = "Keys")]
            public class Keys
            {
                [DataMember]
                public const string BrandID = "BrandID";
            }
        }
    }
}