using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class PropertyTypeDTO
    {
        [DataMember]
        public int CultureID { get; set; }
        [DataMember]
        public byte PropertyTypeID { get; set; }
        [DataMember]
        public string PropertyTypeName { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUMapId { get; set; }

        [DataMember]
        public List<PropertyDTO> PropertyList { get; set; }
    }
}
