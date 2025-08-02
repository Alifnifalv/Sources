using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
   public class ProductToProductMapDTO
    {
        [DataMember]
        public ProductMapDTO FromProduct { get; set; }
        [DataMember]
        public List<ProductMapDTO> ToProduct { get; set; }
        [DataMember]
        public List<SalesRelationshipTypeDTO> SalesRelationshipTypes { get; set; }
        [DataMember]
        public Nullable<int> CreatedBy { get; set; }
        [DataMember]
        public Nullable<int> UpdatedBy { get; set; }
        [DataMember]
        public Nullable<DateTime> CreatedDate { get; set; }   
        [DataMember]
        public Nullable<DateTime> UpdatedDate { get; set; }
    }
}
