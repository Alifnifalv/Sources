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
    public class CountryDTO
    {
        [DataMember]
        public decimal CountryID { get; set; }
        [DataMember]
        public string CountryCode { get; set; }
        [DataMember]
        public string CountryName { get; set; }
        [DataMember]
        public int CurrencyID { get; set; }
        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public Nullable<decimal> ConversionRate { get; set; }
        [DataMember]
        public Nullable<byte> DecimalPlaces { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DataFeedDateTime { get; set; }
        [DataMember]
        public Nullable<bool> IsActiveForCurrency { get; set; }
        [DataMember]
        public string CurrencyCodeDisplayText { get; set; }
        [DataMember]
        public string TelephoneCode { get; set; }

    }
}
