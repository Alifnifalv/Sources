using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Catalog
{
     [DataContract]
    public class ProductInventoryConfigCultureDTO: Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public byte CultureID { get; set; }
          [DataMember]
        public long ProductInventoryConfigID { get; set; }
          [DataMember]
        public string Description { get; set; }
          [DataMember]
        public string Details { get; set; }
    }
}
