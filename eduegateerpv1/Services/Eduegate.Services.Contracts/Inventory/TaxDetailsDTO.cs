using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    [DataContract]
    public class TaxDetailsDTO
    {
        [DataMember]
        public long TaxID { get; set; }
        [DataMember]
        public int? TaxTemplateID { get; set; }
        [DataMember]
        public int? TaxTemplateItemID { get; set; }
        [DataMember]
        public int? TaxTypeID { get; set; }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public int? Percentage { get; set; }
        [DataMember]
        public bool? HasTaxInclusive { get; set; }
        [DataMember]
        public string TaxName { get; set; }
        [DataMember]
        public decimal? InclusiveTaxAmount { get; set; }
        [DataMember]
        public decimal? ExclusiveTaxAmount { get; set; }
    }
}
