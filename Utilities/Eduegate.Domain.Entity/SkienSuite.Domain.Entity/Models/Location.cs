using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class Location
    {
        public Location()
        {
            this.ProductLocationMaps = new List<ProductLocationMap>();
        }

        public long LocationIID { get; set; }
        public string LocationCode { get; set; }
        public string Description { get; set; }
        public Nullable<long> BranchID { get; set; }
        public Nullable<byte> LocationTypeID { get; set; }
        public string Barcode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual LocationType LocationType { get; set; }
        public virtual ICollection<ProductLocationMap> ProductLocationMaps { get; set; }
    }
}
