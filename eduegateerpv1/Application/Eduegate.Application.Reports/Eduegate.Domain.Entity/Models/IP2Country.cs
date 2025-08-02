using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class IP2Country
    {
        public Nullable<long> BeginningIP { get; set; }
        public Nullable<long> EndingIP { get; set; }
        public Nullable<long> AssignedIP { get; set; }
        public string TwoCountryCode { get; set; }
        public string ThreeCountryCode { get; set; }
        public string CountryName { get; set; }
        public int IP2CountryID { get; set; }
    }
}
