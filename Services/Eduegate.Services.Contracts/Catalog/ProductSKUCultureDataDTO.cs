using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductSKUCultureDataDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
          [DataMember]
        public byte CultureID { get; set; }
          [DataMember]
        public long ProductSKUMapID { get; set; }
          [DataMember]
        public string ProductSKUName { get; set; }
          
    }
}
