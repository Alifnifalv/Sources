using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    [DataContract]
    public class TaxSettingDTO : Eduegate.Framework.Contracts.Common.BaseMasterDTO
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
        public List<TaxTemplateItemsDTO> TemplateItems
        {
            get; set;
        }
    }
}
