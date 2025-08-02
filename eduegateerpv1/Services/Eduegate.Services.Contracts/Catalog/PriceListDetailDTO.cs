using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class PriceListDetailDTO
    {
        [DataMember]
        public long ProductPriceListBranchMapIID { get; set; }

        [DataMember]
        public long PriceListID { get; set; }

        [DataMember]
        public string PriceDescription { get; set; }
    }
}