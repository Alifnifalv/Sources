using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Settings
{
    [DataContract]
    public class MenuLinkDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
         [DataMember]
        public long  MenuLinkIID { get; set; }
        [DataMember]
        public long?  ParentMenuID { get; set; }
        [DataMember]
        public string  MenuName { get; set; }
        [DataMember]
        public byte?  MenuLinkTypeID { get; set; }
        [DataMember]
        public string  ActionType { get; set; }
        [DataMember]
        public string  ActionLink { get; set; }
        [DataMember]
        public string  ActionLink1 { get; set; }
        [DataMember]
        public string  ActionLink2 { get; set; }
        [DataMember]
        public string  ActionLink3 { get; set; }
        [DataMember]
        public string  Parameters { get; set; }
        [DataMember]
        public int?  SortOrder { get; set; }
        [DataMember]
        public string  MenuTitle { get; set; }
        [DataMember]
        public string  MenuIcon { get; set; }
        [DataMember]
        public int?  CompanyID { get; set; }
        [DataMember]
        public int?  SiteID { get; set; }
        [DataMember]
        public string  MenuGroup { get; set; }
    }
}


