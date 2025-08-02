using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models
{
    [Table("ProductPriceListCustomerMaps", Schema = "catalog")]
    public partial class ProductPriceListCustomerMap
    {
        [Key]
        public long ProductPriceListCustomerMapIID { get; set; }
        public Nullable<long> ProductPriceListID { get; set; }
        public Nullable<long> CustomerID { get; set; }
        public Nullable<byte> EntitlementID { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        //public byte[] TimeStamps { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ProductPriceList ProductPriceList { get; set; }
        public virtual EntityTypeEntitlement EntityTypeEntitlement { get; set; }
    }
}
