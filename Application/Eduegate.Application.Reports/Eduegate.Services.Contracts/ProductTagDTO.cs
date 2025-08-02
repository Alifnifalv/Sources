using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductTagDTO
    {
        [DataMember]
        public Nullable<decimal> TagIID { get; set; }

        [DataMember]
        public string TagName { get; set; }
    }
}
