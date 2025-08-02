using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common.PageRender;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.MobileAppWrapper
{
    [DataContract]
    public class CategoryProductListDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public BoilerPlateDTO boilerPlateDTO { get; set; }
        [DataMember]
        public string categoryCode { get; set; }
    }
}
