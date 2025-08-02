using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Eduegate.Services.Contracts
{
    [DataContract]
    public class VouchersDTO
    {
        [DataMember]
        public long VoucherIID { get; set; }
        [DataMember]
        public string VoucherNo { get; set; }
        [DataMember]
        public string VoucherPin { get; set; }
        [DataMember]
        public Nullable<byte> VoucherTypeID { get; set; }
        [DataMember]
        public Nullable<bool> IsSharable { get; set; }
        [DataMember]
        public Nullable<long> CustomerID { get; set; }
        [DataMember]
        public Nullable<decimal> Amount { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [DataMember]
        public Nullable<decimal> MinimumAmount { get; set; }
        [DataMember]
        public Nullable<decimal> CurrentBalance { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public Nullable<byte> StatusID { get; set; }

        [DataMember]
        public string VoucherMessage { get; set; }

    }
}
