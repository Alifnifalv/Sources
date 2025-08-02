using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDigitalRevertLog
    {
        public long ProductDigitalRevertID { get; set; }
        public long RefOrderID { get; set; }
        public int RefProductID { get; set; }
        public string DigitalKey { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> ProductDigitalID { get; set; }
    }
}
