using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    public class LanguageDTO
    {
        [DataMember]
        public string LanguageCode { get; set; }
        [DataMember]
        public string DisplayText { get; set; }

        [DataMember]
        public Int32 LanguageID { get; set; }
        [DataMember]
        public byte CultureID { get; set; }
    }
}
