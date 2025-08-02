using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Enums;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class CategoryImageMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CategoryImageMapIID { get; set; }
        [DataMember]
        public Nullable<long> CategoryID { get; set; }
        [DataMember]
        public Nullable<ImageTypes> ImageType { get; set; }
        [DataMember]
        public Nullable<byte> ImageTypeID { get; set; }
        [DataMember]
        public string ImageFile { get; set; }
        [DataMember]
        public string ImageTitle { get; set; }
        [DataMember]
        public string ImageLinkParameters { get; set; }

        [DataMember]
        public string ImageTarget { get; set; }
        [DataMember]
        public Nullable<int> ActionLinkTypeID { get; set; }

        [DataMember]
        public bool isInternalLink { get; set; }
        [DataMember]
        public long SerialNo { get; set; }
        [DataMember]
        public int? CompanyID { get; set; }
        [DataMember]
        public int? SiteID { get; set; }
    }
}
