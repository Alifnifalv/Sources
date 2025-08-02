using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public partial class ProductSKUDTO
    {
        [DataMember]
        public long ProductSKUMapId { get; set; }

        [DataMember]
        public List<PropertyTypeDTO> PropertyTypeList { get; set; }
    }
}
