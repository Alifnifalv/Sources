using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Services.Contracts.Catalog;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class ProductCategoryDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        public ProductCategoryDTO()
        {
            CategoryMarketPlace = new CategoryMarketPlaceDTO();
            CategoryCultureDatas = new List<CategoryCultureDataDTO>();
            CategorySetting = new List<CategorySettingDTO>();
        }

        [DataMember]
        public long CategoryID { get; set; }
        [DataMember]
        public Nullable<long> ParentCategoryID { get; set; }
        [DataMember]
        public string ParentCategoryName { get; set; }
        [DataMember]
        public string CategoryCode { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string ImageName { get; set; }
        [DataMember]
        public string ThumbnailImageName { get; set; }
        [DataMember]
        public List<string> CategoryHierarchies { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public bool Active { get; set; }
        [DataMember]
        public bool IsInNavigationMenu { get; set; }
        [DataMember]
        public string SeoKeyWords { get; set; }
        [DataMember]
        public Nullable<bool> IsPrimary { get; set; }
        [DataMember]
        public string CategoryNameAr { get; set; }
        [DataMember]
        public string SeoKeyWordsAr { get; set; }
        [DataMember]
        public List<CategoryImageMapDTO> ImageMaps { get; set; }
        [DataMember]
        public List<KeyValueDTO> Tags { get; set; }

        [DataMember]
        public CategoryMarketPlaceDTO CategoryMarketPlace { get; set; }

        [DataMember]
        public List<CategoryCultureDataDTO> CategoryCultureDatas { get; set; }

        [DataMember]
        public List<CategorySettingDTO> CategorySetting { get; set; }

        [DataMember]
        public Nullable<bool> IsReporting { get; set; }

    }
}
