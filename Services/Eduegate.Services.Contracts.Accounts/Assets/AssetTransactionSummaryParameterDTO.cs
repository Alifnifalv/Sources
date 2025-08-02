using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts.Accounts.Assets
{
    [DataContract]
    public class AssetTransactionSummaryParameterDTO
    {
        [DataMember]
        public long LoginID { get; set; }

        [DataMember]
        public string DocuementTypeID {get;set;}

        [DataMember]
        public DateTime DateFrom {get;set;}

        [DataMember]
        public DateTime DateTo { get; set; }
    }
}