using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class SupplierSearchView
    {
        public long SupplierIID { get; set; }
        public string Supplier { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNo1 { get; set; }
        public string AddressName { get; set; }
        public Nullable<long> branchiid { get; set; }
        public string branchname { get; set; }
        public string ProductManager { get; set; }
        public string Entitlements { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CompanyName { get; set; }
        public string LoginEmailID { get; set; }
        public Nullable<int> companyID { get; set; }
    }
}
