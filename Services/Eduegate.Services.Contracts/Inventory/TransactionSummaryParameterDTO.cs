using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts.Inventory
{
    [DataContract]
    public class TransactionSummaryParameterDTO
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
