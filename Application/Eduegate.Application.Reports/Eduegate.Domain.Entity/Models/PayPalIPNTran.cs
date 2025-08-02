using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class PayPalIPNTran
    {
        public long RecordID { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string SessionIP { get; set; }
        public string Response { get; set; }
    }
}
