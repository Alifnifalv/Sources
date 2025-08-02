using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class vwCountryGroupList
    {
        public long CountryID { get; set; }
        public int RefGroupID { get; set; }
        public string CountryCode { get; set; }
        public string CountryNameEn { get; set; }
        public string CountryNameAr { get; set; }
        public bool Active { get; set; }
        public int GroupID { get; set; }
        public string GroupType { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> MinWeight { get; set; }
        public Nullable<decimal> MinAmount { get; set; }
        public Nullable<int> AfterMinWeight { get; set; }
        public Nullable<decimal> AfterMinAmount { get; set; }
    }
}
