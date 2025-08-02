using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductFamilyDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductFamilyIID { get; set; }

        [DataMember]
        public string FamilyName { get; set; }

        [DataMember]
        public Nullable<int> ProductFamilyTypeID { get; set; }
        [DataMember]
        public List<PropertyDTO> Properties { get; set; }
        [DataMember]
        public List<PropertyTypeDTO> PropertyTypes { get; set; }
    }
}
