using System;
using System.Runtime.Serialization;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class CurrencyDTO
    {
        [DataMember]
        public int CurrencyID { get; set; }
        [DataMember]
        public Nullable<int> CompanyID { get; set; }
        [DataMember]
        public string ISOCode { get; set; }
        [DataMember]
        public string AnsiCode { get; set; }
        [DataMember]
        public Nullable<int> NumericCode { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Symbol { get; set; }
        [DataMember]
        public Nullable<byte> DecimalPrecisions { get; set; }
        [DataMember]
        public Nullable<decimal> ExchangeRate { get; set; }
        [DataMember]
        public Nullable<bool> IsEnabled { get; set; }
    }
}





