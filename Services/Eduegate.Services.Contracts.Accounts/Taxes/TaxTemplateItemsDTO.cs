using Eduegate.Framework.Contracts.Common;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Taxes
{
    [DataContract]
    public class TaxTemplateItemsDTO : BaseMasterDTO
    {
        [DataMember]
        public int TaxTemplateItemID { get; set; }
        [DataMember]
        public int? TaxTemplateID { get; set; }
        [DataMember]
        public int? TaxTypeID { get; set; }
        [DataMember]
        public long? AccountID { get; set; }
        [DataMember]
        public KeyValueDTO Account { get; set; }
        [DataMember]
        public decimal? Amount { get; set; }
        [DataMember]
        public int? Percentage { get; set; }
        [DataMember]
        public bool? HasTaxInclusive { get; set; }
    }
}