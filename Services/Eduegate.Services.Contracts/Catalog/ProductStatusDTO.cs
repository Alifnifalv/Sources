using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class ProductStatusDTO
    {
        [DataMember]
        public int CultureID { get; set; }

        [DataMember]
        public long StatusIID { get; set; }  

        [DataMember]
        public string StatusName { get; set; }
    }
}
