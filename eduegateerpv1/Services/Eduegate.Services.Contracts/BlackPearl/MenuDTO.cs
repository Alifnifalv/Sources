using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class MenuDTO
    {
        [DataMember]
        public decimal MenuIID { get; set; }

        [DataMember]
        public string MenuDescription { get; set; }
    }
}
