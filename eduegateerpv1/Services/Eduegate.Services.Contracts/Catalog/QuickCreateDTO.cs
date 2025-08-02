using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Framework.Contracts.Common;
using Eduegate.Framework.Contracts.Common;

namespace Eduegate.Services.Contracts.Catalog
{
    [DataContract]
   public class QuickCreateDTO
    {
        [DataMember]
        public long ProductIID { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public string ProductCode { get; set; }
        //[DataMember]
        //public string ProductFamily { get; set; }
        [DataMember]
        public long? BrandIID { get; set; }
        [DataMember]
        public string BrandName { get; set; }
        ////[DataMember]
        ////public BrandDTO Brand { get; set; }
        [DataMember]
        public Nullable<long> ProductFamilyIID { get; set; }
        [DataMember]
        public string ProductFamilyName { get; set; }
        //[DataMember]
        //public ProductFamilyDTO ProductFamily { get; set; }
        [DataMember]
        public Nullable<long> ProductOwnderID { get; set; }
        [DataMember]
        public Nullable<long> StatusIID { get; set; }        
        [DataMember]
        public string KeyWords { get; set; }
        [DataMember]
        public Nullable<bool> IsOnline { get; set; }
        [DataMember]
        public List<CultureDataInfoDTO> CultureInfo { get; set; }
        [DataMember]
        public List<ProductPropertiesTypeValuesDTO> Properties { get; set; }
        [DataMember]
        public List<PropertyDTO> DefaultProperties { get; set; }

        [DataMember]
        public ProductInventoryConfigDTO ProductInventoryConfigDTOs { get; set; }

        [DataMember]
        public long? ProductTypeID { get; set; }
        [DataMember]
        public string StatusName { get; set; }
        //[DataMember]
        //public string ProductType { get; set; }
        [DataMember]
        public int? TaxTemplateID { get; set; }
    }
}
