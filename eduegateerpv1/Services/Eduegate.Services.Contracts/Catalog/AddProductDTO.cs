using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Services.Contracts.Commons;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
   public class AddProductDTO
    {
       [DataMember]
       public QuickCreateDTO QuickCreate { get; set; }

       [DataMember]
       public List<SKUDTO> SKUMappings { get; set; }

       [DataMember]
       public List<ProductCategoryDTO> SelectedCategory { get; set; }
        [DataMember]
       public string ImageSourceTempPath { get; set; }
        [DataMember]
       public string ImageSourceDesignationPath { get; set; }
        [DataMember]
        public string VideoSourceTempPath { get; set; }
        [DataMember]
        public string VideoSourceDestinationPath { get; set; }
        [DataMember]
       public ProductToProductMapDTO ProductMaps { get; set; }

        [DataMember]
        public List<ProductTagDTO> SelectedTags { get; set; }

        [DataMember]
        public SeoMetadataDTO SeoMetadata { get; set; }

        [DataMember]
        public ProductToProductMapDTO ProductBundles { get; set; }

        [DataMember]
        public ProductToProductMapDTO SKUBundles { get; set; }

        [DataMember] 
        public long ID { get; set; }

        [DataMember]
        public long SelectedKeyValueOwnerId { get; set; }

    }
}
