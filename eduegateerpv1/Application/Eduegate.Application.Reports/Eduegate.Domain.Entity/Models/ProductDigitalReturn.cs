using System;
using System.Collections.Generic;

namespace Eduegate.Domain.Entity.Models
{
    public partial class ProductDigitalReturn
    {
        public int ReturnID { get; set; }
        public int RefProductDigitalID { get; set; }
        public string ReturnReason { get; set; }
        public long UserID { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public string RMAVoucherNo { get; set; }
        public Nullable<long> RMAUserID { get; set; }
        public Nullable<System.DateTime> RMAUpdatedOn { get; set; }
        public string Remarks { get; set; }
        public string ProvidedCode1 { get; set; }
        public string ProvidedCode2 { get; set; }
        public string ProvidedCode3 { get; set; }
        public string ProvidedCodeRef1 { get; set; }
        public string ProvidedCodeRef2 { get; set; }
        public string ProvidedCodeRef3 { get; set; }
        public virtual ProductDigital ProductDigital { get; set; }
    }
}
