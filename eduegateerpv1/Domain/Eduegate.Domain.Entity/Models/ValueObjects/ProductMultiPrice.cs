using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Domain.Entity.Models.ValueObjects
{
    [Keyless]
    public class ProductMultiPrice
    {
        public string GroupID { get; set; }
        public string GroupName { get; set; }

        public string MultipriceValue { get; set; }
        public bool isSelected { get; set; }
    }
}
