using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductStatusCultureData
    {
        public byte CoultureID { get; set; }
        public byte ProductStatusID { get; set; }
        public string StatusName { get; set; }
    }
}
