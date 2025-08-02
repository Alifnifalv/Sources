using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Catalog
{
    public class ProductDeliveryCountrySettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
    {
        [DataMember]
        public long ProductDeliveryCountrySettingsIID { get; set; }

        [DataMember]
        public Nullable<long> ProductID { get; set; }

        [DataMember]
        public Nullable<long> ProductSKUMapID { get; set; }

        [DataMember]
        public Nullable<long> CountryID { get; set; }

        [DataMember]
        public string CountryName { get; set; }
    }
    
}
