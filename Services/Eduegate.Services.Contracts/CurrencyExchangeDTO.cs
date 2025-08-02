using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CurrencyExchangeDTO
    {
        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        public int CurrencyID { get; set; }

        [DataMember]
        public string BaseCurrencyCode { get; set; }

        [DataMember]
        public string ExchangeCurrencyCode { get; set; }

        [DataMember]
        public decimal ExchangeRate { get; set; }

        [DataMember]
        public Nullable<DateTime> UpdatedDate { get; set; }
    }
}
