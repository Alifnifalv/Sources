using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public partial class CustomerProductReferenceDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CustomerProductReferenceID { get; set; }
        [DataMember]
        public Nullable<long> CustomerID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }
        [DataMember]
        public string BarCode { get; set; }
    }
}
