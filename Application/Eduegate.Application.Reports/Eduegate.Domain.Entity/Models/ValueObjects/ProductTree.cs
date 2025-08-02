using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    public class ProductTree
    {
        public long ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
    }
}
