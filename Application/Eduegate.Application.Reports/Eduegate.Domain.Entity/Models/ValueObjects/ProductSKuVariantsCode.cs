using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
   public class ProductSKuVariantsCode
    {
        public long ProductSKUMapIID { get; set; }
        public string ProductCode { get; set; }
        public string PropertyTypeName { get; set; }
        public string PropertyName { get; set; }
        public long ProductIID { get; set; }
        public int CultureID { get; set; }
    }
}
