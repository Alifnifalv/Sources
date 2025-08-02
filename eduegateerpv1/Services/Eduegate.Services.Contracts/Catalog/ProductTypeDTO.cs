using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductTypeDTO
    {
        
        [DataMember]
        public int ProductTypeID { get; set; }
        [DataMember]
        public string ProductTypeName { get; set; }
    }
}
