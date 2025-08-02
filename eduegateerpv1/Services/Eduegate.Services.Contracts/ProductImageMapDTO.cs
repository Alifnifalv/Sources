using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductImageMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductSKUMapID { get; set; }
        [DataMember]
        public string GalleryImage { get; set; }
        [DataMember]
        public String ListingImage { get; set; }
        [DataMember]
        public string ZoomImage { get; set; }
        [DataMember]
        public string ThumbnailImage { get; set; }
        
    }
}
