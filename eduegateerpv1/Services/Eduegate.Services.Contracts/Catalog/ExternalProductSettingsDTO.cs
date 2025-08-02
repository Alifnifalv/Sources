using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ExternalProductSettingsDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public Nullable<long> CustomerProductReferenceID { get; set; }
        [DataMember]
        public Nullable<long> CustomerID { get; set; }
        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string PartNo { get; set; }
        [DataMember]
        public string ExternalBarcode { get; set; }
    }
}
