using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class CategoryBrand
    {
        public string KeyCode { get; set; }
        public string KeyType { get; set; }
        public Nullable<byte> CategoryLevel { get; set; }
    }
}
