using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductListingVisitLogIntl
    {
        [Key]
        public int LogID { get; set; }
        public Nullable<short> RefCountryID { get; set; }
        public Nullable<bool> isCategory { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<bool> isBrand { get; set; }
        public Nullable<int> BrandID { get; set; }
        public string SortOrder { get; set; }
        public Nullable<byte> PageIndex { get; set; }
        public Nullable<short> NumRows { get; set; }
        public Nullable<bool> isFixedFilter { get; set; }
        public Nullable<bool> isUserFilter { get; set; }
        public string FixedFilters { get; set; }
        public string UserFilters { get; set; }
        public Nullable<System.DateTime> VisitOn { get; set; }
    }
}
