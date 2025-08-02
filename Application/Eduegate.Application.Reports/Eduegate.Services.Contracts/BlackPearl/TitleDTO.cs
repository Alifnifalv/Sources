using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class TitleDTO
    {
        [DataMember]
        public string TitleID { get; set; }

        [DataMember]
        public string TitleName { get; set; }
    }
}
