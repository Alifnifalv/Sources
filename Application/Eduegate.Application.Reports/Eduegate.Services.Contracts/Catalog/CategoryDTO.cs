using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
    public class CategoryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long CategoryIID { get; set; }
        [DataMember]
        public Nullable<long> ParentCategoryID { get; set; }
        [DataMember]
        public string CategoryCode { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string ImageName { get; set; }
        [DataMember]
        public string ThumbnailImageName { get; set; }
        [DataMember]
        public Nullable<bool> IsActive { get; set; }
        [DataMember]
        public List<CategoryDTO> CategoryList { get; set; }
        [DataMember]
        public List<CategoryImageMapDTO> CategoryImageMapList { get; set; }

        [DataMember]
        public List<BrandDTO> BrandList { get; set; }
        [DataMember]
        public string SlidingImageName { get; set; }
    }
}
