using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Eduegate.Domain.Entity.Models;

namespace Eduegate.Services.Contracts.Eduegates
{
    [DataContract]
    public class CountryMasterDTO
    {
        [DataMember]
        public long CountryID { get; set; }

        [DataMember]
        public int RefGroupID { get; set; }

        [DataMember]
        public string CountryCode { get; set; }

        [DataMember]
        public string CountryNameEn { get; set; }

        [DataMember]
        public string CountryNameAr { get; set; }

        [DataMember]
        public bool Active { get; set; }

        [DataMember]
        public Nullable<bool> Operation { get; set; }

        [DataMember]
        public string BaseCurrency { get; set; }

        [DataMember]
        public Nullable<decimal> ConversionRate { get; set; }

        [DataMember]
        public Nullable<byte> NoofDecimals { get; set; }

        [DataMember]
        public Nullable<System.DateTime> DataFeedDateTime { get; set; }

        [DataMember]
        public int CurrencyID { get; set; }

        [DataMember]
        public string CurrencyCode { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public string CurrencyCodeDisplayText { get; set; }
        [DataMember]
        public string TelephoneCode { get; set; }
    }


    public static class CountryMapper
    {
        public static CountryMasterDTO ToCountryDTOMap(Country obj)
        {
            if (obj == null)
            {
                return new CountryMasterDTO();
            }
            return new CountryMasterDTO()
            {
                CountryID = obj.CountryID,
                CountryCode = obj.ThreeLetterCode,
                CountryNameEn = obj.CountryName,
                CurrencyID = (int)obj.CurrencyID,
                //CurrencyCode = obj.CurrencyCode,
                //CurrencyName = obj.CurrencyName,
                //ConversionRate = obj.ConversionRate,
                //NoofDecimals = obj.DecimalPlaces,
                //DataFeedDateTime = obj.DataFeedDateTime,
                //Active = obj.IsActiveForCurrency.HasValue == true ? (bool)obj.IsActiveForCurrency : false,
                //CurrencyCodeDisplayText = obj.CurrencyCodeDisplayText,
                //TelephoneCode = obj.CurrencyCodeDisplayText,
            };
        }
    }
}
