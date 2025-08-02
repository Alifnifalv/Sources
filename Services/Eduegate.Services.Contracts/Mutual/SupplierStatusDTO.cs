using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Mutual
{
    [DataContract]
    public class SupplierStatusDTO
    {
        [DataMember]
        public byte SupplierStatusID { get; set; }
        [DataMember]
        public string StatusName { get; set; }
    }
}
