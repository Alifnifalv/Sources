using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity
{
    public class ProductDeliveryTypeList
    {
        public long ProductID { get; set; }
        public Nullable<long> SkuID { get; set; }
        public long DeliveryTypeID { get; set; }
        public string DeliveryTypeName { get; set; }
        public long Priority { get; set; }
    }
}
