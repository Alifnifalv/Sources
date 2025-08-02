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
    public class ExternalSettingsDTO
    {
        [DataMember]
        public KeyValueDTO Product { get; set; }

        [DataMember]
        public List<ExternalProductSettingsDTO> ExternalProductSettings { get; set; }


        //For SupplierViewmodel
        [DataMember]
        public string ProductorServiceDescription { get; set; }

        [DataMember]
        public string PricingInformation { get; set; }

        [DataMember]
        public int? LeadTimeDays { get; set; }

        [DataMember]
        public int? MinOrderQty { get; set; }

        [DataMember]
        public string Warranty_GuaranteeInfo { get; set; }

        [DataMember]
        public string PrdctCategories { get; set; }
    }
}
