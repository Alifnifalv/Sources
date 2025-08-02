using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.MenuLinks
{
    [DataContract]
    public class MenuCategoryDTO
    {
        [DataMember]
        public long MenuLinkID { get; set; }
        [DataMember]
        public Nullable<int> SortOrder { get; set; }
        [DataMember]
        public string ActionLink { get; set; }

        [DataMember]
        public long? CategoryIID { get; set; }
        [DataMember]
        public Nullable<long> ParentCategoryID { get; set; }
        [DataMember]
        public string CategoryCode { get; set; }
        [DataMember]
        public string CategoryName { get; set; }

        [DataMember]
        public string ThumbnailImageName { get; set; }
    }
}
