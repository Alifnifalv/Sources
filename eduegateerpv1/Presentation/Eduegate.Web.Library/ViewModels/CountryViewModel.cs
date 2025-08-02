using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eduegate.Web.Library.ViewModels
{
    public class CountryViewModel
    {
        public decimal CountryID { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public Nullable<decimal> ConversionRate { get; set; }
        public Nullable<byte> DecimalPlaces { get; set; }
        public DateTime DataFeedDateTime { get; set; }
        public Nullable<bool> IsActiveForCurrency { get; set; }
        public string CurrencyCodeDisplayText { get; set; }
        public string TelephoneCode { get; set; }
    }
}