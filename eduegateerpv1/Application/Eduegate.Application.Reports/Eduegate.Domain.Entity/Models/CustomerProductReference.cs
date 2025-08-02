using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CustomerProductReference
    {
        public long CustomerProductReferenceIID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<long> ProductSKUMapID { get; set; }
        public string BarCode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public byte[] TimeStamps { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProductSKUMap ProductSKUMap { get; set; }
    }
}
