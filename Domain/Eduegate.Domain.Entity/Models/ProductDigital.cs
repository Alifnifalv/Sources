using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDigital
    {
        public ProductDigital()
        {
            this.ProductDigitalReturns = new List<ProductDigitalReturn>();
        }

        [Key]
        public int ProductDigitalID { get; set; }
        public int RefProductID { get; set; }
        public string DigitalKey { get; set; }
        public Nullable<int> RefOrderID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> DigitalKeySold { get; set; }
        public string SupplierInvoice { get; set; }
        public string DigitalKeyStatus { get; set; }
        public Nullable<short> RefUserID { get; set; }
        public virtual ProductMaster ProductMaster { get; set; }
        public virtual ICollection<ProductDigitalReturn> ProductDigitalReturns { get; set; }
    }
}
