using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.MenuLinks
{
    [DataContract]
    public class SubMenuDTO
    {
        [DataMember]
        public Nullable<long> MenuLinkID { get; set; }
        [DataMember]
        public Nullable<long> SubMenuID { get; set; }
        [DataMember]
        public long? SubMenuParentID { get; set; }
        [DataMember]
        public string SubMenuName { get; set; }
        [DataMember]
        public string ActionLink { get; set; }
        [DataMember]
        public Nullable<int> SortOrder { get; set; }
    }
}
