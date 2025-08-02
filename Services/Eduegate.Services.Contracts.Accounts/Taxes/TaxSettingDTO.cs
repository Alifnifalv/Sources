using Eduegate.Framework.Contracts.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Taxes
{
    [DataContract]
    public class TaxSettingDTO : BaseMasterDTO
    {
        [DataMember]
        public int TaxTemplateID { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        [DataMember]
        public bool? HasTaxInclusive { get; set; }
        [DataMember]
        public bool? IsDefault { get; set; }
        [DataMember]
        public bool? IsActive { get; set; }

        [DataMember]
        public List<TaxTemplateItemsDTO> TemplateItems { get; set; }
    }
}
