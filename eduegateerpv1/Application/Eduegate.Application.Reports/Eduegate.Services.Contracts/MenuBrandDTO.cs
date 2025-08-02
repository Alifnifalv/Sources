using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class MenuBrandDTO
    {
        [DataMember]
        public long BrandIID { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public string Descirption { get; set; }
        [DataMember]
        public string LogoFile { get; set; }
        [DataMember]
        public long MenuLinkID { get; set; }
    }
}
