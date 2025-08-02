using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class BrandDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long BrandIID { get; set; }
        [DataMember]
        public string BrandCode { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public long StatusID { get; set; }
        [DataMember]
        public string BrandLogo { get; set; }
        public int BrandPosition { get; set; }
        public string BrandKeywordsEn { get; set; }
        [DataMember]
        public string BrandNameAr { get; set; }
        public string BrandKeywordsAr { get; set; }
        [DataMember]
        public List<KeyValueDTO> Tags { get; set; }
         [DataMember]
        public List<BrandImageMapDTO> ImageMaps { get; set; }
        [DataMember]
        public Nullable<bool> Status { get; set; }
        [DataMember]
        public string ErrorCode { get; set; }
        [DataMember]
        public bool IsError { get; set; }
    }
}
