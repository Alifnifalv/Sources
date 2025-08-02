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
    public class BrandImageMapDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO    
    {
        [DataMember]
        public long BrandImageMapIID { get; set; }
        [DataMember]
        public Nullable<long> BrandID { get; set; }
        [DataMember]
        public Nullable<ImageTypes> ImageType { get; set; }
        [DataMember]
        public Nullable<byte> ImageTypeID { get; set; }
        [DataMember]
        public string ImageFile { get; set; }
        [DataMember]
        public string ImageTitle { get; set; }
    }
}
