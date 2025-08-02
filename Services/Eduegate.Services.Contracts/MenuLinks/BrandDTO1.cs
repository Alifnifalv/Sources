using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.MenuLinks
{
    [DataContract]
    public class BrandDTO1
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
        public Nullable<long> MenuLinkID { get; set; }
        [DataMember]
        public string ActionLink { get; set; }
        [DataMember]
        public Nullable<int> SortOrder { get; set; }
    }
}
