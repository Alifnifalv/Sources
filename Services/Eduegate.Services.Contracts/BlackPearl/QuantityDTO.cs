using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class QuantityDTO
    {
        [DataMember]
        public decimal QuantityIID { get; set; }

        [DataMember]
        public int QuantityValue { get; set; }
    }
}
